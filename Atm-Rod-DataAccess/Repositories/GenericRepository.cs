using Atm_Rod_Entities.Interface.Repositories;
using Atm_Rod_Repository.DbUnit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BankDbContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(BankDbContext context)
        {
            _databaseContext = context;
            _dbSet = context.Set<T>();
        }
        public async Task<int> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                await SaveAsync();
            }
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<List<T>> GetAllAsync(bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            var result = await SaveAsync();
            return result;
        }

        public async Task<int> SaveAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }
    }
}
