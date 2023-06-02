using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementMVC.Models.ViewModel;
using BlogManagementMVC.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace BlogManagementMVC.Services
{
    public class BlogService : Service, IBlogService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string blogUrl;

        public BlogService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            blogUrl = configuration.GetValue<string>("ServiceUrls:Admin");
        }

        public Task<T> CreateAsync<T>(BlogDTO blogDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = blogDTO,
                Url = blogUrl + "api/BlogController",
            });
        }
            
        public Task<T> DeleteAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = blogUrl + "api/BlogController" + Id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = blogUrl + "api/BlogController"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = blogUrl + "api/BlogController" + id,
            });
        }

        public Task<T> UpdateAsync<T>(BlogDTO blogDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = blogDTO,
                Url = blogUrl + "api/BlogController"
            });
        }

    }
}
