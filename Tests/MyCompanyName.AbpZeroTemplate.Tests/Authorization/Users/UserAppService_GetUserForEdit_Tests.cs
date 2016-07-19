﻿using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Tawh.NoTrace.Authorization.Roles;
using Tawh.NoTrace.Authorization.Users;
using Shouldly;
using Xunit;

namespace Tawh.NoTrace.Tests.Authorization.Users
{
    public class UserAppService_GetUserForEdit_Tests : UserAppServiceTestBase
    {
        [Fact]
        public async Task Should_Work_For_NonExisting_User()
        {
            //Arrange
            LoginAsHostAdmin();

            //Act
            var output = await UserAppService.GetUserForEdit(new NullableIdInput<long>());

            //Assert
            output.User.Id.ShouldBe(null);
            output.User.Name.ShouldBe(null);
            output.User.Password.ShouldBe(null);

            output.Roles.Length.ShouldBe(1);
            output.Roles.Any(r => r.RoleName == StaticRoleNames.Host.Admin).ShouldBe(true);
            output.Roles.Single(r => r.RoleName == StaticRoleNames.Host.Admin).IsAssigned.ShouldBe(true);
        }

        [Fact]
        public async Task Should_Work_For_Existing_User()
        {
            //Arrange
            var adminUser = await GetUserByUserNameOrNullAsync(User.AdminUserName);
            var managerRole = await CreateRoleAsync("Manager");
            var roleCount = UsingDbContext(context => context.Roles.Count(r => r.TenantId == AbpSession.TenantId));

            //Act
            var output = await UserAppService.GetUserForEdit(new NullableIdInput<long> { Id = adminUser.Id });

            //Assert
            output.User.Id.ShouldBe(adminUser.Id);
            output.User.Name.ShouldBe(adminUser.Name);
            output.User.Password.ShouldBe(null);

            output.Roles.Length.ShouldBe(roleCount);
            var managerRoleDto = output.Roles.FirstOrDefault(r => r.RoleName == managerRole.Name);
            managerRoleDto.ShouldNotBe(null);
            managerRoleDto.RoleId.ShouldBe(managerRole.Id);
            managerRoleDto.IsAssigned.ShouldBe(false);

            var adminRoleDto = output.Roles.FirstOrDefault(r => r.RoleName == StaticRoleNames.Tenants.Admin);
            adminRoleDto.ShouldNotBe(null);
            adminRoleDto.IsAssigned.ShouldBe(true);
        }

        protected async Task<Role> CreateRoleAsync(string roleName)
        {
            return await UsingDbContextAsync(async context => context.Roles.Add(new Role(AbpSession.TenantId, roleName, roleName)));
        }
    }
}
