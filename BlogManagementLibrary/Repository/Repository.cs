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
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _db;
        public DbSet<T> _dbSet;
        public Repository(ApplicationDBContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }
        public async Task CreateAsync(T e)
        {
            await _db.AddAsync(e);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> query = _dbSet;
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _dbSet;
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T e)
        {
            _dbSet.Remove(e);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T e)
        {
            _db.Update(e);
            await _db.SaveChangesAsync();
            return e;
        }
    }
}
