using BlogManagementLibrary.Model;
using BlogManagementLibrary;
using BlogManagementMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using BlogManagementLibrary.Model.Dto;
using BlogManagementMVC.Services.IServices;

namespace BlogManagementMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService)
        {
            _logger = logger;
            _blogService = blogService;
        }
        public IActionResult Index()
        {

            List<Blog> list = new();
            var blog = _blogService.GetAllAsync<Blog>();
            if (blog != null)
            {
                list = JsonConvert.DeserializeObject<List<Blog>>(Convert.ToString(blog.Result));

            }
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}