using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Models.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
namespace BlogApp.Models.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        BlogDbContext _context;
        public UnitOfWork()
        {
            _context = new BlogDbContext();
        }
        private IGenericRepository<Post> _posts;
        public Repositories.IGenericRepository<Post> Posts
        {
            get
            {
                if (_posts == null)
                {
                    return new EfGenericRepository<Post>(_context);
                }
                return _posts;
            }
        }
        private IGenericRepository<ApplicationUser> _users;
        public Repositories.IGenericRepository<ApplicationUser> Users
        {
            get
            {
                if (_users == null)
                {
                    return new EfGenericRepository<ApplicationUser>(_context);
                }
                return _users;
            }
        }

        private IGenericRepository<IdentityRole> _roles;
        public Repositories.IGenericRepository<IdentityRole> Roles
        {
            get
            {
                if (_roles == null)
                {
                    return new EfGenericRepository<IdentityRole>(_context);
                }
                return _roles;
            }
        }
        private IGenericRepository<Tag> _tags;
        public Repositories.IGenericRepository<Tag> Tags
        {
            get
            {
                if (_tags == null)
                {
                    return new EfGenericRepository<Tag>(_context);
                }
                return _tags;
            }
        }
        private IGenericRepository<Job> _jobs;
        public Repositories.IGenericRepository<Job> Jobs
        {
            get
            {
                if (_jobs == null)
                {
                    return new EfGenericRepository<Job>(_context);
                }
                return _jobs;
            }
        }
        private IGenericRepository<ApplyJob> _ApplyJobs;
        public Repositories.IGenericRepository<ApplyJob> ApplyJobs
        {
            get
            {
                if (_ApplyJobs == null)
                {
                    return new EfGenericRepository<ApplyJob>(_context);
                }
                return _ApplyJobs;
            }
        }
        private IGenericRepository<Notification> _notfication;
        public Repositories.IGenericRepository<Notification> Notifications
        {
            get
            {
                if (_notfication == null)
                {
                    return new EfGenericRepository<Notification>(_context);
                }
                return _notfication;
            }
        }
        private IGenericRepository<ContactUs> _contactUses;
        public Repositories.IGenericRepository<ContactUs> ContactUses
        {
            get
            {
                if (_contactUses == null)
                {
                    return new EfGenericRepository<ContactUs>(_context);
                }
                return _contactUses;
            }
        }

        private IGenericRepository<CustService> _customerservices;
        public Repositories.IGenericRepository<CustService> Customerservices
        {
            get
            {
                if (_customerservices == null)
                {
                    return new EfGenericRepository<CustService>(_context);
                }
                return _customerservices;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        private IGenericRepository<Comment> _comments;
        public IGenericRepository<Comment> Comments
        {
            get
            {
                if (_comments == null)
                {
                    return new EfGenericRepository<Comment>(_context);
                }
                return _comments;
            }
        }
    }
}