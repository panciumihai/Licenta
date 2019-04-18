using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusManagement.Business.Generics
{
    public interface ICreateService<in TCreateEntity> where TCreateEntity : class
    {
        Task<Guid> AddAsync(TCreateEntity entity);
        Task AddAsync(IEnumerable<TCreateEntity> entities);

        Task UpdateAsync(TCreateEntity entity);

        Task DeleteAsync(TCreateEntity entity);
        Task DeleteAsync(IEnumerable<TCreateEntity> entities);
    }
}
