using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Model.Dto
{
    public class BlogDTO
    {
        [Key]
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
