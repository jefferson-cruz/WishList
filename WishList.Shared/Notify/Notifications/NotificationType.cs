namespace WishList.Shared.Notify.Notifications
{
    public enum NotificationType
    {
        Conflict = 409,
        Failure = 500,
        NotFound = 404,
        Created = 201,
        Ok = 200,
        Violation = 400
    }
}
