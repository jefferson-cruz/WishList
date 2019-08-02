using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Shared.Result
{
    public interface IResultBase
    {
        bool Success { get; }
        bool Failure { get; }
        IResultBase Result { get; }
        //IReadOnlyCollection<Notification> Notifications { get; }
    }

    public interface IResultBase<T> : IResultBase
    {
        T Value { get; }
    }
}
