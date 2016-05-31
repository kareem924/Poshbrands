using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        //
        // GET: /Roles/
        public ActionResult Index()
        {
            var context = new BlogDbContext();

            var allRoles = context.Roles.Select(role => new RoleViewModel
            {
                RoleName = role.Name
            });
            return View(allRoles);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RoleViewModel newRole)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new BlogDbContext()));

                if (!roleManager.RoleExists(newRole.RoleName))
                {
                    IdentityResult result = roleManager.Create(new IdentityRole(newRole.RoleName));

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already existed!");
                }
            }
            return View(newRole);
        }

       
    }
}