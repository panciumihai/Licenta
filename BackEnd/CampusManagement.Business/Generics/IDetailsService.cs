using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CampusManagement.Business.Generics
{
    public interface IDetailsService<TEntityDetails> where TEntityDetails : class
    {
        Task<TEntityDetails> GetAsync(Guid id);
        Task<IEnumerable<TEntityDetails>> GetAllAsync();
        Task<IEnumerable<TEntityDetails>> FindAsync(Expression<Func<TEntityDetails, bool>> expression);
    }
}
