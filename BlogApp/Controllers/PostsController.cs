using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using BlogApp.Models.UnitOfWork;
using System.Data.Entity;

namespace BlogApp.Controllers
{

    public class PostsController : Controller
    {
        IUnitOfWork _uow;
        public PostsController()
        {
            _uow = new UnitOfWork(); // Database
        }
        public PostsController(IUnitOfWork uof) // Fake 
        {
            _uow = uof;
        }
        //
        // GET: /Posts/
        [AllowAnonymous]
        public ActionResult Index(int page = 0) //0 1 2  4
        {
            var pageSize = 10;
            var model = _uow.Posts.List()
                .OrderByDescending(p => p.CreatedDate)
                .Skip(pageSize * page).Take(pageSize);

            ViewBag.PreviousPage = (page - 1) < 0 ? 0 : page - 1;
            ViewBag.NextPage = page + 1;

            return View(model);
        }
        //
        // GET: /Posts/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id) // 0  101 fdgdgdgd   null
        {
            if (id.HasValue)
            {
                var model = _uow.Posts.Find(id.Value);
                if (model != null)
                    return View(model);
            }
            // Response.StatusCode = 404;
            return HttpNotFound(); // return Not Found (IIS)  
        }

        [AllowAnonymous]
        [ActionName("Comments")]
        public JsonResult DisplayCommentsByPostID(int id)
        {
            if (id > 0)
            {
                var comments = _uow.Comments.List(comment => comment.PostID == id);
                return Json(comments, JsonRequestBehavior.AllowGet);
            }
            return Json(new List<Comment>(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AddComment(string body, int postID)
        {
            Comment newComment = new Comment() { Body = body, PostID = postID };
            _uow.Comments.Add(newComment);
            _uow.Save();
            return Json(newComment);
        }
        //
        // GET: /Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Posts/Create
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Create([Bind(Exclude = "Tags")] Post postToCreate, string tags) //, Tag newTag)
        {
            try
            {
                //"MVC,Razor,ASP.NET"
                if (ModelState.IsValid)
                {
                    string[] alltags = tags.Split(new char[] { ',' });

                    foreach (var tag in alltags)
                    {
                        var existedTag = GetTagIfExisted(tag.Trim());
                        if (existedTag != null)
                        {
                            postToCreate.Tags.Add(existedTag);
                        }
                        else
                        {
                            postToCreate.Tags.Add(new Tag { Name = tag.Trim() });
                        }
                    }
                    // TODO: Add insert logic here
                    _uow.Posts.Add(postToCreate);
                    _uow.Save();
                    //
                    return RedirectToAction("Index");
                }
                return View("Create", postToCreate);
            }
            catch
            {
                return View("error");
            }
        }

        //
        // GET: /Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = _uow.Posts.Find(id);
            if (model != null)
                return View(model);
            return HttpNotFound();
        }

        //
        // POST: /Posts/Edit/5
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult Edit(int id, Post postToUpdate, string tags)
        {
            try
            {
                var existedPost = _uow.Posts.Find(id);
                if (existedPost != null)
                {
                    existedPost.Title = postToUpdate.Title;
                    existedPost.Content = postToUpdate.Content;
                    existedPost.Tags.Clear();

                    string[] alltags = tags.Split(new char[] { ',' });

                    foreach (var tag in alltags)
                    {
                        var existedTag = GetTagIfExisted(tag.Trim());
                        if (existedTag != null)
                        {
                            existedPost.Tags.Add(existedTag);
                        }
                        else
                        {
                            existedPost.Tags.Add(new Tag { Name = tag.Trim() });
                        }
                    }

                    _uow.Posts.Edit(id, existedPost);
                    _uow.Save();
                    return RedirectToAction("Index");
                }
                // TODO: Add update logic here
                return HttpNotFound();
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var model = _uow.Posts.Find(id);
            if (model != null)
                return View(model);
            return HttpNotFound();
        }

        //
        // POST: /Posts/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, Post postToDelete)
        {
            try
            {
                // TODO: Add delete logic here
                _uow.Posts.Delete(id);
                _uow.Save();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private Tag GetTagIfExisted(string tagName)
        {
            var selectedTag = _uow.Tags.List(tag => tag.Name == tagName).FirstOrDefault();
            return selectedTag;
        }

        [AllowAnonymous]
        public ActionResult Tags(int id)
        {
            var postsByTag = _uow.Posts.List(post => post.Tags.Where(tag => tag.ID == id).Count() > 0);
            return View("Index", postsByTag);
        }
        [Authorize]
        public JsonResult AutoTags()
        {
            var results = _uow.Tags.List().Select(t => t.Name);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}
