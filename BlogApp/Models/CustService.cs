using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class CustService
    {
        [Key]
        public int CustomerServiceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Preference { get; set; }
        public string Message { get; set; }
        public string Brand { get; set; }
        public string Branch { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}