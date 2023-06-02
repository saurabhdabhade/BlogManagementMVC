using BlogManagementLibrary.Model;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementMVC.Models.ViewModel
{
    public class BlogRequest : Blog
    {
        public int BlogId { get; set; }
        [Required]
        public string BlogName { get; set; }
        [Required]
        public string BlogContent { get; set; }
        public string BlogCategory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int NoOfSubscriptions { get; set; }
    }
}
