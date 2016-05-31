using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class ApplyJob
    {
        [Key]
        public int JobApplyId { get; set; }
        public int JobId { get; set; }
        [Required]

        public string EmployeeName { get; set; }
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter proper contact details.")]
        public string EmployeeMobile { get; set; }
        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "You should type a valid mail")]
        public string EmployeeEmail { get; set; }
        public string EmployeeCv { get; set; }
        public DateTime ApplyDate { get; set; }
    }
}