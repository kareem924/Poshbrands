using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using BlogApp.Models;
using BlogApp.Models.UnitOfWork;
using PagedList;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _uow;

        public HomeController()
        {
            _uow = new UnitOfWork();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(ContactUs contactUsToApply)
        {
            if (ModelState.IsValid)
            {
                contactUsToApply.CreatedAt = DateTime.Now;
                _uow.ContactUses.Add(contactUsToApply);
                _uow.Save();
                ViewBag.Message = "Your Message has been sent Successfully!";
                ViewBag.Status = "success";
                var lastId = _uow.ContactUses.Last().ContactUsId;
                _uow.Notifications.Add(new Notification() { IsDissmissed = false, Title = contactUsToApply.Name+ " "+"has sent a message", Type = 2, TypeId = lastId, Contraller = "Home", Action = "ContactUsDetail", CreatedAt = DateTime.Now });
                _uow.Save();
                return View();
            }

            ViewBag.Message = "Please, correct your info , and try again !";
            ViewBag.Status = "danger";

            return View("ContactUs", contactUsToApply);
        }
        [Authorize(Roles = "contact us")]
        public ActionResult ContactUsSent(int? page)
        {
            var contactus = _uow.ContactUses.List();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfConatctUs = contactus.OrderBy(p => p.CreatedAt).ToPagedList(pageNumber, 14); // will only contain 25 products max because of the pageSize
            ViewBag.onePageOfcontactUss = onePageOfConatctUs;

            return View();
        }
        [Authorize(Roles = "contact us")]
        public ActionResult ContactUsDetail(int id)
        {
            var contactUsMessage = _uow.ContactUses.Find(id);
            return View(contactUsMessage);
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult MediaCenter()
        {
            return View();
        }
        public ActionResult OurBrands()
        {
            return View();
        }
        public ActionResult TermsOfUse()
        {
            return View();
        }
        public ActionResult Policy()
        {
            return View();
        }
    }
}