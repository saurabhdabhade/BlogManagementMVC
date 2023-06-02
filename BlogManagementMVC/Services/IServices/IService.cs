using AutoMapper.Internal;
using BlogManagementLibrary.Model;
using BlogManagementMVC.Models.ViewModel;

namespace BlogManagementMVC.Services.IServices
{
    public interface IService
    {
        APIResponse responseMode { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
