using BlogManagementLibrary.Model;
using System.ComponentModel.DataAnnotations;

namespace BlogManagementMVC.Models.ViewModel
{
    public class UserRequest : User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
