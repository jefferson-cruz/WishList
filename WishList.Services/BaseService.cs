using System.Collections.Generic;
using WishList.Services.Interfaces;
using WishList.Shared.Notify;
using WishList.Shared.Notify.Notifications;

namespace WishList.Services
{
    public class BaseService : Notify, IBaseService
    {
        public new IReadOnlyCollection<Notification> Notifications => base.Results;

        public bool HasResults => base.HasResults;
    }
}
