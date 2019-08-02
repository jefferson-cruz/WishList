using System.Collections.Generic;
using System.Linq;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Result
{
    public abstract class SuccessResult : ResultBase
    {
        public SuccessResult() : base(true, string.Empty)
        {

        }

        public SuccessResult(IReadOnlyCollection<Notification> notifications) : base(true, notifications)
        {

        }
    }

    public abstract class SuccessResult<T> : ResultBase<T>
    {
        public SuccessResult() : base(true, null)
        {

        }

        public SuccessResult(T value) : base(value, true, string.Empty)
        {

        }

        public SuccessResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, true, notifications)
        {

        }
    }

    public class OkResult : SuccessResult
    {
        public OkResult()
        {

        }
    }

    public class OkResult<T> : SuccessResult<T>
    {
        public OkResult() 
        {

        }
        public OkResult(T value) : base(value)
        {

        }

        public OkResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class CreatedResult : SuccessResult
    {
        public CreatedResult()
        {

        }
    }

    public class CreatedResult<T> : SuccessResult<T>
    {
        public CreatedResult(T value) : base(value)
        {

        }

        public CreatedResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }

    public class NoContentResult : SuccessResult
    {
        public NoContentResult()
        {

        }
    }

    public class NoContentResult<T> : SuccessResult<T>
    {
        public NoContentResult(T value) : base(value)
        {

        }

        public NoContentResult(T value, IReadOnlyCollection<Notification> notifications) : base(value, notifications)
        {

        }
    }
}
