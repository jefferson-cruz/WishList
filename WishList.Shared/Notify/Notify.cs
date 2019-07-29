using System;
using System.Collections.Generic;
using System.Linq;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Notify
{
    public class Notify
    {
        private readonly List<Notification> notifications = new List<Notification>();

        public IReadOnlyCollection<Notification> Notifications => this.notifications;

        public bool HasNotification => this.notifications.Any();

        public bool HasNoNotification => !HasNotification;

        public void AddNotification(string message) 
        {
            this.notifications.Add(new Notification(message));
        }

        public void AddNotification<TNotification>(string message) where TNotification : Notification
        {
            this.notifications.Add((TNotification)Activator.CreateInstance(typeof(TNotification), message));
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications) => this.notifications.AddRange(notifications);
    }
}
