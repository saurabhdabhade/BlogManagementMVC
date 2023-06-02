using BlogManagementLibrary.Model;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementMVC.Models.ViewModel
{
    public class AdminRequest : Admin
    {
        public int AdminId { get; set; }
        [Required]
        public string AdminName { get; set; }
        [Required]
        public string AdminPassword { get; set; }
    }
}
