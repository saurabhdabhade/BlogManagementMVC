using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using System.Linq.Expressions;

namespace BlogManagementMVC.Services.IServices
{
    public interface IBlogService
    {
        Task<T> CreateAsync<T>(BlogDTO blogDTO);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(BlogDTO blogDTO);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
