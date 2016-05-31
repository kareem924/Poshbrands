using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Job
    {
        [Key]
        public int? JobId { get; set; }
        public string JobeName { get; set; }
        public string JobDescription { get; set; }
        public string JobQualifications { get; set; }
        public string JobResponsibilities { get; set; }
        public bool Available { get; set; }
        public string Location { get; set; }
        public string Employmentstatus { get; set; }
        public string Category { get; set; }
        public string RolePurpose { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime DatePosted { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd.MM.yy}", ApplyFormatInEditMode = true)]
        public DateTime ClosingDate { get; set; }
    }
}