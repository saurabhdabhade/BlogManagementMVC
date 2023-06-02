using AutoMapper;
using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Repository;
using BlogManagementMVC.Models.ViewModel;
using BlogManagementMVC.Services;
using BlogManagementMVC.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementMVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;

        public BlogController(IBlogService blogService , ApplicationDBContext dbContext, IMapper mapper)
        {
            _blogService = blogService;
            _dbContext = dbContext;
            _mapper = mapper;
         }
        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult GetAllBlogs(BlogRequest blogRequest)
        {
            // Mapping BlogRequest To Blog Model
            var blog = new BlogDTO
            {
                //BlogId = blogRequest.BlogId,
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };

            _blogService.GetAllAsync<Blog>();
            return View("Add");
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult CreateBlog(BlogRequest blogRequest)
        {
            // Mapping BlogRequest To Blog Model
            var blog = new BlogDTO
            {
                //BlogId = blogRequest.BlogId,
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };

            _blogService.CreateAsync<Blog>(blog);
            return View("Add");
        }

        [HttpDelete]
        [ActionName("Delete")]
        public IActionResult Delete(BlogRequest blogRequest)
        {
            var blog = new BlogDTO
            {
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };

            _blogService.DeleteAsync<Blog>(blog.BlogId);
            return View("Delete");
        }

        [HttpPut]
        [ActionName("Edit")]
        public  IActionResult Edit(BlogRequest blogRequest)
        {
            var blog = new BlogDTO
            {
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };

             _blogService.UpdateAsync<Blog>(blog);
             return View("Edit");
        }

        [HttpPut]
        public IActionResult Subscribe(BlogRequest blogRequest)
        {
            var blog = new BlogDTO
            {
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };
            if (blog.NoOfSubscriptions == 0)
            {
                Console.WriteLine("Sorry! There Is No Subscriptions Available");
            }
            else
            {
                var blogData = _blogService.GetAsync<Blog>(blog.BlogId);
                Blog blogs = blogData.Result;
                _mapper.Map<BlogDTO, Blog>(blog);
                int n = blogs.NoOfSubscriptions - 1;
                blogs.NoOfSubscriptions = n;
                _blogService.UpdateAsync<Blog>(blog);
            }
            return View();

        }

        [HttpPut]
        public IActionResult UnSubscribe(BlogRequest blogRequest)
        {
            var blog = new BlogDTO
            {
                BlogName = blogRequest.BlogName,
                BlogContent = blogRequest.BlogContent,
                BlogCategory = blogRequest.BlogCategory,
                CreatedAt = blogRequest.CreatedAt,
                UpdatedAt = blogRequest.UpdatedAt,
                NoOfSubscriptions = blogRequest.NoOfSubscriptions
            };
            if (blog.NoOfSubscriptions == 0)
            {
                Console.WriteLine("Sorry! There Is No Subscriptions Available");
            }
            else
            {
                var blogData = _blogService.GetAsync<Blog>(blog.BlogId);
                Blog blogs = blogData.Result;
                _mapper.Map<BlogDTO, Blog>(blog);
                int n = blogs.NoOfSubscriptions + 1;
                blogs.NoOfSubscriptions = n;
                _blogService.UpdateAsync<Blog>(blog);
            }
            return View();
        }
    }
}
