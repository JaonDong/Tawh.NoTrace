using System.Threading.Tasks;
using Abp.Notifications;
using Tawh.NoTrace.Authorization.Users;
using Tawh.NoTrace.MultiTenancy;

namespace Tawh.NoTrace.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(long userId, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
