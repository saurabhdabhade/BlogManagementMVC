using BlogManagementLibrary.Model.Dto;
using BlogManagementLibrary.Model;
using System.Linq.Expressions;

namespace BlogManagementMVC.Services.IServices
{
    public interface IUserService
    {
        Task<T> CreateAsync<T>(UserDTO dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> UpdateAsync<T>(UserDTO dto);
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
    }
}
