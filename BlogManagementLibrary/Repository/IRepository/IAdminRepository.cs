using BlogManagementLibrary.Model;
using BlogManagementLibrary.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Repository.IRepository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task CreateAsync(Admin Entity);
        Task SaveAsync();
        Task<List<Admin>> GetAllAsync(Expression<Func<Admin, bool>>? filter);
        Task<Admin> GetAsync(Expression<Func<Admin, bool>>? filter);
        Task<Admin> UpdateAsync(Admin Entity);
        Task RemoveAsync(Admin Entity);
        Task<IEnumerable<Admin>> GetAllAsync();
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<Admin> GetAsync(int id);


    }
}
