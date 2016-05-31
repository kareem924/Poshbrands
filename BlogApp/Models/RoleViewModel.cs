﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        //public string UserName { get; set; }

        public string RoleID { get; set; }
    }
}