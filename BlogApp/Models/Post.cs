using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public class Post
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string Content { get; set; }
        [Display(Name = "Post Title")]
        [MaxLength(200)]
        [MinLength(10)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}