using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Result
{
    public abstract class FailureResult : ResultBase
    {
        public FailureResult(IReadOnlyCollection<Notification> notifications) : base(false, notifications)
        {

        }

        public FailureResult(string message) : base(false, message)
        {

        }
    }

    public abstract class FailureResult<T> : ResultBase<T>
    {
        public FailureResult(string message) : base(false, message)
        {

        }

        public FailureResult(T value, string message) : base (value, false, message)
        {

        }

        public FailureResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, false, notifications)
        {

        }
    }

    public class BadRequestResult: FailureResult
    {
        public BadRequestResult(string message) : base(message)
        {

        }
        public BadRequestResult(IReadOnlyCollection<Notification> notifications) : base(notifications)
        {

        }
    }

    public class BadRequestResult<T> : FailureResult<T>
    {
        public BadRequestResult(string message) : base(message)
        {

        }
        public BadRequestResult(T value, string message) : base(value, message)
        {

        }

        public BadRequestResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class UnauthorizedResult : FailureResult
    {
        public UnauthorizedResult(IReadOnlyCollection<Notification> notifications) : base(notifications)
        {

        }
    }

    public class UnauthorizedResult<T> : FailureResult<T>
    {
        public UnauthorizedResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class NotFoundResult : FailureResult
    {
        public NotFoundResult(IReadOnlyCollection<Notification> notifications) : base(notifications)
        {

        }
    }

    public class NotFoundResult<T> : FailureResult<T>
    {
        public NotFoundResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class ConflictResult : FailureResult
    {
        public ConflictResult(string message) : base(message)
        {

        }
        public ConflictResult(IReadOnlyCollection<Notification> notifications) : base(notifications)
        {

        }
    }

    public class ConflictResult<T> : FailureResult<T>
    {
        public ConflictResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class InternalServerErrorResult : FailureResult
    {
        public InternalServerErrorResult(string message) : base(message)
        {

        }

        public InternalServerErrorResult(IReadOnlyCollection<Notification> notifications) : base(notifications)
        {

        }
    }

    public class InternalServerErrorResult<T> : FailureResult<T>
    {
        public InternalServerErrorResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }
}
