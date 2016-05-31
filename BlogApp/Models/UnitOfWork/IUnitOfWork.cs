using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Models.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
namespace BlogApp.Models.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Post> Posts { get; }

        IGenericRepository<Tag> Tags { get; }

        IGenericRepository<ApplicationUser> Users { get; }
        IGenericRepository<IdentityRole> Roles { get; }

        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Job> Jobs { get; }
        IGenericRepository<ApplyJob> ApplyJobs { get; }
        IGenericRepository<Notification> Notifications { get; }
        IGenericRepository<ContactUs> ContactUses { get; }
        IGenericRepository<CustService> Customerservices { get; }
        void Save(); //Commit

    }
}
