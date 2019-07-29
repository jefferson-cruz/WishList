using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult CreateResponse(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                switch (notification.Type)
                {
                    case NotificationType.Failure:
                        return StatusCode(StatusCodes.Status500InternalServerError, notification);

                    case NotificationType.NotFound:
                        return StatusCode(StatusCodes.Status404NotFound, notification);

                    case NotificationType.Conflict:
                        return StatusCode(StatusCodes.Status409Conflict, notification);

                    case NotificationType.Violation:
                        return StatusCode(StatusCodes.Status400BadRequest, notification);
                }
            }

            throw new System.InvalidOperationException($"Is not possible determinate type of notification in {nameof(notifications)} collection");
        }
    }
}