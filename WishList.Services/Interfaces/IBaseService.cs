using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;
using WishList.Shared.Result;

namespace WishList.Services.Interfaces
{
    public interface IBaseService
    {
        IReadOnlyCollection<IResultBase> Results { get; }
        bool HasResults { get; }
    }
}
