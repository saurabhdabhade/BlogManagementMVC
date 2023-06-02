using BlogManagementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task CreateAsync(User Entity);
        Task SaveAsync();
        Task<List<User>> GetAllAsync(Expression<Func<User, bool>>? filter);
        Task<User> GetAsync(Expression<Func<User, bool>>? filter);
        Task<User> UpdateAsync(User Entity);
        Task RemoveAsync(User Entity);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
