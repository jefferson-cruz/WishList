namespace WishList.Shared.Notify.Notifications
{
    public class Violation : Notification
    {
        public Violation(string message) : base(message, NotificationType.Violation)
        {
        }
    }
}
