using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WishList.Shared.Notify.Notifications
{
    [NotMapped]
    public class Notification
    {
        public Notification(string message)
        {
            Message = message;
            CreateAt = DateTime.Now;
        }

        public string Message { get; }
        public DateTime CreateAt { get; }
    }
}
