using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models.UnitOfWork;
using PagedList;

namespace BlogApp.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        IUnitOfWork _uow;

        public NotificationController()
        {
            _uow = new UnitOfWork();
        }
        public ActionResult Index(int? page, int? conPage, int? cusPage)
        {
            var jobsNotifications = _uow.Notifications.List(x => x.Type == 1);
            var pageNumber = page ?? 1;
            var onePageOfJobs = jobsNotifications.OrderByDescending(p => p.CreatedAt).ToPagedList(pageNumber, 14);
            ViewBag.onePageOfJobs = onePageOfJobs;

            var contactUsNotifications = _uow.Notifications.List(x => x.Type == 2);
            var contactUspageNumber = conPage ?? 1;
            var onePageOfContactUs = contactUsNotifications.OrderByDescending(p => p.CreatedAt).ToPagedList(contactUspageNumber, 14);
            ViewBag.onePageOfContactUs = onePageOfContactUs;

            var customerNotifications = _uow.Notifications.List(x => x.Type == 3);
            var customerpageNumber = cusPage ?? 1;
            var onePageOfCustomer = customerNotifications.OrderByDescending(p => p.CreatedAt).ToPagedList(customerpageNumber, 14);
            ViewBag.onePageOfCustomer = onePageOfCustomer;
            return View();
        }
        public ActionResult MarkNotificationAsRead(int id)
        {
            try
            {
                var model = _uow.Notifications.Find(id);
                model.IsDissmissed = true;
                _uow.Notifications.Edit(id, model);
                _uow.Save();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetNotification(int id)
        {
            var userNotification = _uow.Notifications.Find(id);
            if (userNotification == null)
            {
                return new HttpNotFoundResult();
            }

            return PartialView("_RenderNotificationsPreview", userNotification);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _uow.Notifications.Delete(id);
                _uow.Save();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

        }
    }
}