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
    public class UserService : Service, IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string userUrl;

        public UserService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            userUrl = configuration.GetValue<string>("ServiceUrls:Admin");

        }

        public Task<T> CreateAsync<T>(UserDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = dto,
                Url = userUrl + "/api/UserController",
            });
        }

        public Task<T> DeleteAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = userUrl + "/api/UserController" + Id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = userUrl + "/api/UserController",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = userUrl + "/api/UserController" + id,
            });
        }

        public Task<T> UpdateAsync<T>(UserDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = dto,
                Url = userUrl + "/api/UserController"
            });
        }
    }
}
