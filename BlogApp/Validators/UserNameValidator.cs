using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BlogApp.Models;

namespace BlogApp.Validators
{
    public class UserNameValidator
    {
        public static ValidationResult IsUserAvailable(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                //ASP.NET Identity ...............
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new BlogDbContext()));
                var user = userManager.FindByName(userName);
                if (user != null)
                    return new ValidationResult("User Already Existed!");
            }
            return ValidationResult.Success;
        }
    }
}
