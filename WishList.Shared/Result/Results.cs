using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Result
{
    public static class Results
    {
        public static OkResult Ok() => new OkResult();
        public static OkResult<T> Ok<T>(T value, IReadOnlyCollection<Notification> notifications = null) => new OkResult<T>(value, notifications);

        public static CreatedResult Created() => new CreatedResult();
        public static CreatedResult<T> Created<T>(T value, IReadOnlyCollection<Notification> notifications = null) => new CreatedResult<T>(value, notifications);

        public static NoContentResult NoContent() => new NoContentResult();
        public static NoContentResult<T> NoContent<T>(T value, IReadOnlyCollection<Notification> notifications = null) => new NoContentResult<T>(value, notifications);

        public static BadRequestResult BadRequest(IReadOnlyCollection<Notification> notifications) => new BadRequestResult(notifications);

        public static BadRequestResult<T> BadRequest<T>(T value, string message) => new BadRequestResult<T>(value, message);
        public static BadRequestResult<T> BadRequest<T>(T value, IReadOnlyCollection<Notification> notifications) => new BadRequestResult<T>(value, notifications);

        public static UnauthorizedResult Unauthorized(IReadOnlyCollection<Notification> notifications) => new UnauthorizedResult(notifications);
        public static UnauthorizedResult<T> Unauthorized<T>(T value, IReadOnlyCollection<Notification> notifications) => new UnauthorizedResult<T>(value, notifications);

        public static ConflictResult Conflict(string message) => new ConflictResult(message);
        public static ConflictResult Conflict(IReadOnlyCollection<Notification> notifications) => new ConflictResult(notifications);
        public static ConflictResult<T> Conflict<T>(T value, IReadOnlyCollection<Notification> notifications) => new ConflictResult<T>(value, notifications);

        public static InternalServerErrorResult InternalServerError(string message) => new InternalServerErrorResult(message);
        public static InternalServerErrorResult InternalServerError(IReadOnlyCollection<Notification> notifications) => new InternalServerErrorResult(notifications);
        public static InternalServerErrorResult<T> InternalServerError<T>(T value, IReadOnlyCollection<Notification> notifications) => new InternalServerErrorResult<T>(value, notifications);
    }
}
