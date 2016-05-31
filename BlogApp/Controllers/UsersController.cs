using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using BlogApp.Models.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace BlogApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        IUnitOfWork _uow;
        public UsersController()
            : this(new UnitOfWork())
        {

        }
        public UsersController(IUnitOfWork uow)
        {
            this._uow = uow;
        }
        //
        // GET: /Users/
        public ActionResult Index()
        {
            var allUsers = _uow.Users.List().Select(user => new Models.UserRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(role => new Models.RoleViewModel
                {
                    RoleID = role.RoleId,
                    RoleName = role.Role.Name
                }).ToList()
            });
            return View(allUsers);
        }

        public ActionResult Edit(string userid)
        {
            var selectedUser = _uow.Users.List(user => user.Id == userid).Select(user => new Models.UserRoleViewModel
            {
                UserName = user.UserName,
                Roles = user.Roles.Select(role => new Models.RoleViewModel
                {
                    RoleID = role.RoleId,
                    RoleName = role.Role.Name
                }).ToList()
            }).FirstOrDefault();

            ViewBag.roleName = new SelectList(_uow.Roles.List(), "Name", "Name");

            ViewBag.UserRoles = new SelectList(selectedUser.Roles, "RoleName", "RoleName");

            return View("edit", selectedUser);
        }

        public ActionResult AddToRole(string userId, string roleName)
        {
            var result = this.AddUserToRole(userId, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been added to role successfully!.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.Edit(userId);
        }

        public ActionResult RemoveFromRole(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BlogDbContext()));

            var result = userManager.RemoveFromRole(userId, roleName);
            if (result.Succeeded)
            {
                ViewBag.Message = "User has been removed from role successfully!.";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return this.Edit(userId);
        }
        private IdentityResult AddUserToRole(string userId, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BlogDbContext()));
            IdentityResult result;
            if (!userManager.IsInRole(userId, roleName))
            {
                result = userManager.AddToRole(userId, roleName);
            }
            else
            {
                string message = string.Format("User is already existed in {0} Role", roleName);
                result = new IdentityResult(message);
            }
            return result;
        }
    }
}