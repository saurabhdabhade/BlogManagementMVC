using BlogManagementLibrary.Data;
using BlogManagementLibrary.Model;
using BlogManagementLibrary.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagementLibrary.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<Blog> _blogs;
        public BlogRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _blogs = _dbContext.Blogs;
        }
        public Task CreateAsync(Blog Entity)
        {
            _dbContext.AddAsync(Entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<List<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? filter)
        {
            IQueryable<Blog> query = _blogs;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            IQueryable<Blog> query = _blogs;
            return await query.ToListAsync();

        }

        public async Task<Blog> GetAsync(Expression<Func<Blog, bool>>? filter)
        {
            IQueryable<Blog> query = _blogs;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task RemoveAsync(Blog Entity)
        {
            _dbContext.Remove(Entity);
            return SaveAsync(); 
        }

        public async Task SaveAsync()
        {
             await _dbContext.SaveChangesAsync();
        }

        public async Task<Blog> UpdateAsync(Blog Entity)
        {
            _dbContext.Update(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        } 
    }
}
