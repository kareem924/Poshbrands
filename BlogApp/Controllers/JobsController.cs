using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using BlogApp.Models.UnitOfWork;
using PagedList;

namespace BlogApp.Controllers
{
    public class JobsController : Controller
    {
        IUnitOfWork _uow;
        //  IUnitOfWork _uow;
        public JobsController()
        {
            _uow = new UnitOfWork();
        }
        public JobsController(IUnitOfWork uof) // Fake 
        {
            _uow = uof;
        }
        // GET: /Jobs/
        [AllowAnonymous]
        public ActionResult Home()

        {
            var model = _uow.Jobs.List().OrderByDescending(p => p.JobId).Take(5);
            return View(model);

        }
        [AllowAnonymous]
        public ActionResult Job(int jobTitle)
        {
            ViewBag.Id = jobTitle;
            if (jobTitle == 1)
            {
                var model = _uow.Jobs.List(p => p.Available == true);

                return View(model);
            }
            else if (jobTitle == 2)
            {
                var model = _uow.Jobs.List();
                return View(model);
            }
            return View();
        }
        //
        [Authorize(Roles = "job")]
        public ActionResult Index(int? page)
        {
            var Jobs = _uow.Jobs.List();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfJobs = Jobs.OrderBy(p => p.JobeName).ToPagedList(pageNumber, 14); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfJobs = onePageOfJobs;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "job")]
        public ActionResult Index(string JobeName, int? page)
        {
            var Categories = _uow.Jobs.List(p => p.JobeName.Contains(JobeName));
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfJobs = Categories.OrderBy(p => p.JobeName).ToPagedList(pageNumber, 14); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfJobs = onePageOfJobs;
            return View();
        }
        [Authorize(Roles = "job")]
        public ActionResult Create()
        {

            return View();
        }

        [Authorize(Roles = "job")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Job jobToCreate)
        {

            try
            {

                //"MVC,Razor,ASP.NET"
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here

                    _uow.Jobs.Add(jobToCreate);
                    _uow.Save();
                    //
                    return RedirectToAction("Index");
                }
                return View("Create", jobToCreate);
            }
            catch
            {
                return View("error");
            }
        }

        [Authorize(Roles = "job")]
        public ActionResult Edit(int id)
        {

            var model = _uow.Jobs.Find(id);
            if (model != null)
                return View(model);
            return HttpNotFound();
        }

        [Authorize(Roles = "job")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Job jobToUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here

                    _uow.Jobs.Edit(id, jobToUpdate);
                    _uow.Save();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "job")]
        public ActionResult Delete(int id)
        {
            _uow.Jobs.Delete(id);
            _uow.Save();
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateApplyJob(int jobId)
        {

            ViewBag.JobId = jobId;
            ViewBag.JobsDetails = _uow.Jobs.Find(jobId);

            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult CreateApplyJob(ApplyJob applyJobToCreate)
        {

            //try
            //{
            ViewBag.JobId = applyJobToCreate.JobId;

            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    applyJobToCreate.EmployeeCv = file.FileName;

                    string targetFolder = HttpContext.Server.MapPath("~/Content/CV");
                    string targetPath = Path.Combine(targetFolder, applyJobToCreate.EmployeeCv);
                    file.SaveAs(targetPath);
                    //candidate.JobTitle 
                    //candidate.Mail 

                    // Database  / Mail 
                    applyJobToCreate.ApplyDate = DateTime.Now;
                    _uow.ApplyJobs.Add(applyJobToCreate);
                    _uow.Save();
                    ViewBag.Message = "Your Application has been sent Successfully!";
                    ViewBag.Status = "success";
                    ViewBag.JobsDetails = _uow.Jobs.Find(applyJobToCreate.JobId);
                    var jobName = _uow.Jobs.Find(applyJobToCreate.JobId).JobeName;
                    _uow.Notifications.Add(new Notification() { IsDissmissed = false, Title = applyJobToCreate.EmployeeName +" "+ "has sent his CV on " + jobName, Type = 1, TypeId = applyJobToCreate.JobId, Contraller = "Jobs", Action = "AppliedJob", CreatedAt = DateTime.Now });
                    _uow.Save();

                    return View("CreateApplyJob");
                }
                else
                {
                    ViewBag.Message = "Please select your CV!";
                    ViewBag.Status = "danger";
                    ViewBag.JobsDetails = _uow.Jobs.Find(applyJobToCreate.JobId);
                    return View("CreateApplyJob", applyJobToCreate);
                }
            }
            else
            {
                ViewBag.Message = "Please, correct your info , and try again !";
                ViewBag.Status = "danger";
                ViewBag.JobsDetails = _uow.Jobs.Find(applyJobToCreate.JobId);
                return View("CreateApplyJob", applyJobToCreate);
            }


        }
        [Authorize(Roles = "job")]
        public FileResult Download(int id)
        {
            var model = _uow.ApplyJobs.Find(id);
            if (model.EmployeeCv == null)
            {
                return null;
            }
            return File(Path.Combine(Server.MapPath("~/Content/cv"), Path.GetFileName(model.EmployeeCv)), "application/msword", model.EmployeeCv);
        }
        [Authorize]
        public ActionResult AppliedJob(int? page, int id)
        {
            var ApplysJob = _uow.ApplyJobs.List(p => p.JobId == id);
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfApplyJob = ApplysJob.OrderByDescending(p => p.ApplyDate).ToPagedList(pageNumber, 14); // will only contain 25 products max because of the pageSize

            ViewBag.onePageOfApplyJob = onePageOfApplyJob;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Possibilities()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult BecomePosh()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Whywork()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Vacancies()
        {
            var model = _uow.Jobs.List();
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult cando()
        {
            return View();
        }

    }
}