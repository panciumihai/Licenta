using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CampusManagement.Business;
using CampusManagement.Domain.Entities;
using CampusManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace CampusManagement.Persistance
{
    public class CampusManagementContext : DbContext, IGenericRepository
    {
        public CampusManagementContext(DbContextOptions<CampusManagementContext> options)
            : base(options)
        {
            Database.Migrate();
            //Database.EnsureCreated();
        }

        internal DbSet<Student> Students { get; private set; }

        public async Task<T> GetAsync<T>(Guid id) where T : Entity
        {
            return await Set<T>().FirstOrDefaultAsync(t => t.Id == id && t.Available);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : Entity
        {
            return await Set<T>().Where(t => t.Available).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> expression) where T : Entity
        {
            return await Set<T>().Where(t=> t.Available).Where(expression).ToListAsync();
        }

        public async Task<Guid> AddAsync<T>(T entity) where T : Entity
        {
            await Set<T>().AddAsync(entity);
            return entity.Id;
        }

        public async Task AddAsync<T>(IEnumerable<T> entites) where T : Entity
        {
            await Set<T>().AddRangeAsync(entites);
        }

        public async Task UpdateAsync<T>(T entity) where T : Entity
        {
            var exist = await GetAsync<T>(entity.Id);
            if (exist != null)
                Entry(exist).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync<T>(Guid id) where T : Entity
        {
            var entity = await GetAsync<T>(id);
            if (entity != null)
            {
                entity.Delete();
                Entry(entity).CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : Entity
        {
            foreach (var id in ids)
                await DeleteAsync<T>(id);
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}
