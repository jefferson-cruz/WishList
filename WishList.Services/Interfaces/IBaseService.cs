using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Services.Interfaces
{
    public interface IBaseService
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }
    }
}
