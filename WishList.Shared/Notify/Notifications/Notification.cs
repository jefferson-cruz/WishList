using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishList.Shared.Notify.Notifications
{
    [NotMapped]
    public class Notification
    {
        protected Notification() { }

        public Notification(string message) : this(message, NotificationType.Notification)
        {
        }

        public Notification(string message, NotificationType type)
        {
            Message = message;
            Type = type;
            CreateAt = DateTime.Now;
        }

        public string Message { get; }
        public NotificationType Type { get; }
        public string TypeDescripion => Type.ToString();
        public DateTime CreateAt { get; }
    }
}
