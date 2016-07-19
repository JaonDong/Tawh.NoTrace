using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Notifications;

namespace Tawh.NoTrace.Notifications.Dto
{
    public class NotificationSubscriptionDto : IDto
    {
        [Required]
        [MaxLength(NotificationInfo.MaxNotificationNameLength)]
        public string Name { get; set; }

        public bool IsSubscribed { get; set; }
    }
}