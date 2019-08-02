using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Result
{
    public abstract class ResultBase : IResultBase
    {
        private readonly List<Notification> notifications;

        public bool Success { get; }

        public bool Failure => !Success;

        //public IReadOnlyCollection<Notification> Notifications => notifications;

        public IResultBase Result { get; }

        public ResultBase(bool success, string message)
        {
            this.notifications = new List<Notification>();

            Success = success;

            this.notifications.Add(new Notification(message));
        }

        public ResultBase(bool success, IReadOnlyCollection<Notification> notifications)
        {
            this.notifications = new List<Notification>();

            Success = success;

            if (notifications != null)
                this.notifications.AddRange(notifications);
        }
    }

    public abstract class ResultBase<T> : ResultBase, IResultBase<T>
    {
        public T Value { get; }

        public ResultBase(bool success, string message) : base(success, message)
        {

        }

        public ResultBase(T value, bool success, string message) : base(success, message)
        {
            Value = value;
        }

        public ResultBase(T value, bool success, IReadOnlyCollection<Notification> notifications) : base(success, notifications)
        {
            Value = value;
        }
    }
}
