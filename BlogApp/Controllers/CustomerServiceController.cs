using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using BlogApp.Models.UnitOfWork;
using PagedList;

namespace BlogApp.Controllers
{
    public class CustomerServiceController : Controller
    {
        IUnitOfWork _uow;

        public CustomerServiceController()
        {
            _uow = new UnitOfWork();
        }
        // GET: CustomerService
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CustService serviceToApply)
        {
            if (ModelState.IsValid)
            {
                serviceToApply.CreatedAt = DateTime.Now;
                _uow.Customerservices.Add(serviceToApply);
                _uow.Save();
                ViewBag.Message = "Your Message has been sent Successfully!";
                ViewBag.Status = "success";
                var lastId = _uow.Customerservices.Last().CustomerServiceId;
                _uow.Notifications.Add(new Notification() { IsDissmissed = false, Title = serviceToApply.FirstName + " " + "has sent you a message", Type = 3, TypeId = lastId, Contraller = "CustomerService", Action = "CustServicesDetail", CreatedAt = DateTime.Now });
                _uow.Save();

                return View();
            }

            ViewBag.Message = "Please, correct your info , and try again !";
            ViewBag.Status = "danger";

            return View("Index", serviceToApply);
        }
        [Authorize(Roles = "customer service")]
        public ActionResult CustServicesSent(int? page)
        {
            var contactus = _uow.Customerservices.List();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfConatctUs = contactus.OrderBy(p => p.CreatedAt).ToPagedList(pageNumber, 14); // will only contain 25 products max because of the pageSize
            ViewBag.onePageOfcontactUss = onePageOfConatctUs;

            return View();
        }
        [Authorize(Roles = "customer service")]
        public ActionResult CustServicesDetail(int id)
        {
            var contactUsMessage = _uow.Customerservices.Find(id);
            return View(contactUsMessage);
        }
    }
}