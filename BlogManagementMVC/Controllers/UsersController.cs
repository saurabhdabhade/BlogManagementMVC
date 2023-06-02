using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementMVC.Models.ViewModel;
using BlogManagementMVC.Services;
using BlogManagementMVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult User()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Login(UserRequest userRequest)
        {
            // Mapping AdminRequest To Admin Model
            var user = new UserDTO
            {
                UserName = userRequest.UserName,
                UserEmail = userRequest.UserEmail
            };

            _userService.CreateAsync<User>(user);
            return View("Add");  
        }

       /* [HttpDelete]
        [ActionName("Delete")]
        public IActionResult DeleteAdmin(AdminRequest adminRequest)
        {
            // Mapping AdminRequest To Admin Model
            var admin = new Admin
            {
                AdminName = adminRequest.AdminName,
                AdminPassword = adminRequest.AdminPassword
            };

            _db.Admins.Remove(admin);
            _db.SaveChanges();
            return View("Delete");
        }*/
    }
}
