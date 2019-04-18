using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CampusManagement.Entities;

namespace CampusManagement.Business
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(Guid id) where T : Entity;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : Entity;
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> expression) where T : Entity;

        Task<Guid> AddAsync<T>(T entity) where T : Entity;
        Task AddAsync<T>(IEnumerable<T> entites) where T : Entity;

        Task UpdateAsync<T>(T entity) where T : Entity;

        Task DeleteAsync<T>(Guid id) where T : Entity;
        Task DeleteAsync<T>(IEnumerable<Guid> ids) where T : Entity;

        Task SaveAsync();
    }
}
