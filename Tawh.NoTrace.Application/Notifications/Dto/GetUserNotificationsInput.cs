using Abp.Notifications;
using Tawh.NoTrace.Dto;

namespace Tawh.NoTrace.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}