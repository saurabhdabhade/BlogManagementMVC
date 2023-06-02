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
using static System.Reflection.Metadata.BlobBuilder;

namespace BlogManagementLibrary.Repository
{
    public class UserRepository : IUserRepository 
    {
        private readonly ApplicationDBContext _dbContext;
        internal DbSet<User> _user;
        public UserRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _user = _dbContext.Users;
        }
        public Task CreateAsync(User Entity)
        {
            _dbContext.AddAsync(Entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            IQueryable<User> query = _user;
            return await query.ToListAsync();
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>>? filter)
        {
            IQueryable<User> query = _user;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>>? filter)
        {
            IQueryable<User> query = _user;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task RemoveAsync(User Entity)
        {
            _dbContext.Remove(Entity);
            return SaveAsync();
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User Entity)
        {
            _dbContext.Update(Entity);
            await _dbContext.SaveChangesAsync();
            return Entity;
        }
    }
}
