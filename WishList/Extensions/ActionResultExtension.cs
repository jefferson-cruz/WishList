using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WishList.Shared.Notify.Notifications;

namespace WishList.Extensions
{
    public static class ActionResultExtension
    {
        public static IActionResult ToResult(this ControllerBase controller, IEnumerable<Notification> notifications)
        {
            //foreach (var notification in notifications)
            //{
            //    switch (notification.Type)
            //    {
            //        case NotificationType.Failure:
            //            return controller.StatusCode(StatusCodes.Status500InternalServerError, notification);

            //        case NotificationType.NotFound:
            //            return controller.StatusCode(StatusCodes.Status404NotFound, notification);

            //        case NotificationType.Conflict:
            //            return controller.StatusCode(StatusCodes.Status409Conflict, notification);

            //        case NotificationType.Violation:
            //            return controller.StatusCode(StatusCodes.Status400BadRequest, notification);

            //        case NotificationType.Created:
            //            return controller.StatusCode(StatusCodes.Status201Created);

            //        default:
            //            return controller.StatusCode(StatusCodes.Status200OK);
            //    }
            //}

            throw new System.Exception("Notification Type is not defined");
        }
    }
}
