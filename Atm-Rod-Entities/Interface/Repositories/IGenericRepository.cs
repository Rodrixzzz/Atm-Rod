﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atm_Rod_Entities.Interface.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T?>> GetAllAsync(bool tracked = true);
        Task <int> UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task<int> SaveAsync();
    }
}
