using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
namespace BlogApp.Models
{
    public class BlogDbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
        protected override void Seed(BlogDbContext context)
        {
            context.Posts.Add(new Post
            {
                Title = "Change Layout and Runtime in MVC ",
                Content = "Something Something Something Something Something Something Something Something Something Something Something Something Something ",
                CreatedDate = DateTime.Now,
                Tags = new List<Tag>
                 {
                      new Tag{ Name = "ASP.NET "},
                      new Tag{ Name = "MVC"} ,
                      new Tag{ Name = "Razor"} 
                 }
            });

            for (int i = 0; i < 100; i++)
            {
                context.Posts.Add(new Post
                {
                    Title = "Test For Pagination for MVC 5 " + i,
                    Content = "Something Something Something Something Something Something Something Something Something Something Something Something Something ",
                    CreatedDate = DateTime.Now
                });

            }
            context.Roles.Add(new IdentityRole("Managers"));
            context.Roles.Add(new IdentityRole("Authors"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser { UserName = "admin" };

            userManager.Create(user, "123123");

            userManager.AddToRole(user.Id, "Managers");

            context.SaveChanges();

            base.Seed(context);
        }
    }
}