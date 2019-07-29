namespace WishList.Shared.Notify.Notifications
{
    public class NotFound : Notification
    {
        public NotFound(string message) : base(message, NotificationType.NotFound)
        {
        }
    }
}
