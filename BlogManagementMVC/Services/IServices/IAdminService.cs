using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using System.Linq.Expressions;

namespace BlogManagementMVC.Services.IServices
{
    public interface IAdminService
    {
        Task<T> CreateAsync<T>(AdminDTO  adminDTO);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(AdminDTO adminDTO);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
