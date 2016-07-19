using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using Tawh.NoTrace.Authorization.Roles;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.Editions;
using Tawh.NoTrace.MultiTenancy.Demo;
using Abp.Extensions;
using Abp.Notifications;
using Tawh.NoTrace.Notifications;

namespace Tawh.NoTrace.MultiTenancy
{
    /// <summary>
    /// Tenant manager.
    /// </summary>
    public class TenantManager : AbpTenantManager<Tenant, Role, User>
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IUserEmailer _userEmailer;
        private readonly TenantDemoDataBuilder _demoDataBuilder;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IAppNotifier _appNotifier;

        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IUnitOfWorkManager unitOfWorkManager,
            RoleManager roleManager,
            IUserEmailer userEmailer,
            TenantDemoDataBuilder demoDataBuilder, 
            UserManager userManager, 
            INotificationSubscriptionManager notificationSubscriptionManager, 
            IAppNotifier appNotifier) :
            base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager
            )
        {
            _unitOfWorkManager = unitOfWorkManager;
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _demoDataBuilder = demoDataBuilder;
            _userManager = userManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _appNotifier = appNotifier;
        }

        public async Task<int> CreateWithAdminUserAsync(string tenancyName, string name, string adminPassword, string adminEmailAddress, bool isActive, int? editionId, bool shouldChangePasswordOnNextLogin, bool sendActivationEmail)
        {
            int newTenantId;
            long newAdminId;

            using (var uow = _unitOfWorkManager.Begin())
            {
                //Create tenant
                var tenant = new Tenant(tenancyName, name) { IsActive = isActive, EditionId = editionId };
                CheckErrors(await CreateAsync(tenant));
                await _unitOfWorkManager.Current.SaveChangesAsync(); //To get new tenant's id.

                //We are working entities of new tenant, so changing tenant filter
                using (_unitOfWorkManager.Current.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, tenant.Id))
                {
                    //Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get static role ids

                    //grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    //User role should be default
                    var userRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.User);
                    userRole.IsDefault = true;
                    CheckErrors(await _roleManager.UpdateAsync(userRole));

                    //Create admin user for the tenant
                    if (adminPassword.IsNullOrEmpty())
                    {
                        adminPassword = User.CreateRandomPassword();
                    }

                    var adminUser = User.CreateTenantAdminUser(tenant.Id, adminEmailAddress, adminPassword);
                    adminUser.ShouldChangePasswordOnNextLogin = shouldChangePasswordOnNextLogin;
                    adminUser.IsActive = isActive;

                    CheckErrors(await _userManager.CreateAsync(adminUser));
                    await _unitOfWorkManager.Current.SaveChangesAsync(); //To get admin user's id

                    //Assign admin user to admin role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser.Id, adminRole.Name));

                    //Notifications
                    await _appNotifier.WelcomeToTheApplicationAsync(adminUser);

                    //Send activation email
                    if (sendActivationEmail)
                    {
                        adminUser.SetNewEmailConfirmationCode();
                        await _userEmailer.SendEmailActivationLinkAsync(adminUser, adminPassword);
                    }

                    await _unitOfWorkManager.Current.SaveChangesAsync();

                    await _demoDataBuilder.BuildForAsync(tenant);

                    newTenantId = tenant.Id;
                    newAdminId = adminUser.Id;
                }

                await uow.CompleteAsync();
            }

            //Used a second UOW since UOW above sets some permissions and _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync needs these permissions to be saved.
            using (var uow = _unitOfWorkManager.Begin())
            {
                using (_unitOfWorkManager.Current.SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, newTenantId))
                {
                    await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(newTenantId, newAdminId);
                    await _unitOfWorkManager.Current.SaveChangesAsync();
                }

                await uow.CompleteAsync();
            }

            return newTenantId;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
