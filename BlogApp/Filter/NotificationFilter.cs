using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models.UnitOfWork;

namespace BlogApp.Filter
{
    public class NotificationFilter : ActionFilterAttribute
    {
        IUnitOfWork _uow;

        public NotificationFilter()
        {
            _uow = new UnitOfWork();
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated) return;
            var jobsnotifications = _uow.Notifications.List(x => x.Type == 1 && !x.IsDissmissed).Count();
            var contactUsnotifications = _uow.Notifications.List(x => x.Type == 2 && !x.IsDissmissed).Count();
            var customerServicenotifications = _uow.Notifications.List(x => x.Type == 3 && !x.IsDissmissed).Count();
            var notifications = _uow.Notifications.List(x =>!x.IsDissmissed).Count();
            filterContext.Controller.ViewBag.jobNotifications = jobsnotifications;
            filterContext.Controller.ViewBag.contactUsNotifications = contactUsnotifications;
            filterContext.Controller.ViewBag.CustomerServiceNotifications = customerServicenotifications;
            filterContext.Controller.ViewBag.notifications = notifications;
        }
    }
}