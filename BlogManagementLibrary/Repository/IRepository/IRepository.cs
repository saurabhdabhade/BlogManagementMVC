using BlogManagementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T Entity);
        Task SaveAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter);
        Task<T> UpdateAsync(T Entity);
        Task RemoveAsync(T Entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
