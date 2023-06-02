using BlogManagementLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Repository.IRepository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task CreateAsync(Blog Entity);
        Task SaveAsync();
        Task<List<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? filter);
        Task<Blog> GetAsync(Expression<Func<Blog, bool>>? filter);
        Task<Blog> UpdateAsync(Blog Entity);
        Task RemoveAsync(Blog Entity);
        Task<IEnumerable<Blog>> GetAllAsync();
    }
}
