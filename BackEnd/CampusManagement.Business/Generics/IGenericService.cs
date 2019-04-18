using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

namespace CampusManagement.Business
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        IGenericRepository GenericRepository { get; set; }
        IMapper Mapper { get; set; }

        Task<TEntity> GetAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        //Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);

        //Task AddAsync(TEntity entity);
        //Task AddAsync(IEnumerable<TEntity> entites);

        //Task UpdateAsync(TEntity entity);

        //Task DeleteAsync(TEntity entity);
        //Task DeleteAsync(IEnumerable<TEntity> entites);
    }
}
