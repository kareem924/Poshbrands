using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Candidate
    {
        [Required]
        public string Mail { get; set; }
        public string JobTitle { get; set; }
        public HttpPostedFileBase CV { get; set; }
    }
}