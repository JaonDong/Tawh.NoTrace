using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace Tawh.NoTrace.Notifications.Dto
{
    public class GetNotificationsOutput : PagedResultOutput<UserNotification>
    {
        public int UnreadCount { get; set; }

        public GetNotificationsOutput(int totalCount, int unreadCount, List<UserNotification> notifications)
            :base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}