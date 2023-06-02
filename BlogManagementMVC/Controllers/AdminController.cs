using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementMVC.Models.ViewModel;
using BlogManagementMVC.Services;
using BlogManagementMVC.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementMVC.Controllers
{
    public class AdminController : Controller
    {
         private readonly IAdminService _adminService;
         private readonly ApplicationDBContext _dbContext;
        public AdminController(IAdminService adminService, ApplicationDBContext dbContext)
        {
            _adminService = adminService;
            _dbContext = dbContext;
        }

       public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Submit(AdminRequest adminRequest)
        {
            // Mapping AdminRequest To Admin Model
            var admin = new AdminDTO
            {
                AdminName = adminRequest.AdminName,
                AdminPassword = adminRequest.AdminPassword,
                Role = adminRequest.Role,
            };

            _adminService.CreateAsync<Admin>(admin);
         //   _dbContext.SaveChanges();
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
