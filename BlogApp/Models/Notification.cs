using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public bool IsDissmissed { get; set; }
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public string Contraller { get; set; }
        public string Action { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}