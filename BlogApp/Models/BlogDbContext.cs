using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BlogApp.Models
{
    public class ApplicationUser : IdentityUser
    {
    }
    public class BlogDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogDbContext()
            : base("BlogDbContext")
        {

        }
        public virtual IDbSet<Post> Posts { get; set; }
        public virtual IDbSet<Tag> Tags { get; set; }
        public IDbSet<Comment> Comments { get; set; }
        public virtual IDbSet<Page> Pages { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<ApplyJob> ApplyJobs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ContactUs> ContactUses { get; set; }
        public DbSet<CustService> CustServices { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            Database.SetInitializer<BlogDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}