namespace WishList.Shared.Notify.Notifications
{
    public class Conflict : Notification
    {
        public Conflict(string message) : base(message, NotificationType.Conflict)
        {
        }
    }
}
