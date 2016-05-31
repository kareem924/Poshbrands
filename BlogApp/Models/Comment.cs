using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string Body { get; set; }
        
       // public virtual Post Post { set; get; }
        public int PostID { get; set; }
    }
}