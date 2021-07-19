using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarooYar.Models;
using Microsoft.EntityFrameworkCore;

namespace DarooYar.Utilities
{
    public static class RepositoryWrapper
    {
        private static ApplicationContext _applicationContext;
        public static BaseRepository<T> GetBaseRepository<T>() where T : class
        {
            if (_applicationContext == null)
                _applicationContext = new ApplicationContext();
            return new BaseRepository<T>(_applicationContext);
        }
    }
    public class BaseRepository<T> where T : class 
    {
        public DbSet<T> Entities { get; }
        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();
        private readonly ApplicationContext _applicationContext;
        public BaseRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            Entities = _applicationContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await TableNoTracking.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _applicationContext.Set<T>().AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
