using BlogManagementLibrary;
using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using BlogManagementMVC.Models.ViewModel;
using BlogManagementMVC.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BlogManagementMVC.Services
{
    public class AdminService : Service, IAdminService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string admintUrl;

        public AdminService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            admintUrl = configuration.GetValue<string>("ServiceUrls:Admin");

        }

        public Task<T> CreateAsync<T>(AdminDTO adminDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Post",
                Data = adminDTO,
                Url ="https://localhost:7206/api/AdminController",
            });
        }

        public Task<T> DeleteAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Delete",
                Url = admintUrl + "/api/AdminController" + Id,
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = admintUrl + "/api/AdminController",
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Get",
                Url = admintUrl + "/api/AdminController" + id,
            });
        }

        public Task<T> UpdateAsync<T>(AdminDTO adminDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = "Put",
                Data = adminDTO,
                Url = admintUrl + "/api/AdminController"
            });
        }

    }
}
