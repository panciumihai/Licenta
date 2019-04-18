using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Entities;

namespace CampusManagement.Business.Generics
{
    public class DetailsService<TEntity, TEntityDetails> : IDetailsService<TEntityDetails>
        where TEntity : Entity
        where TEntityDetails : class
    {
        private IGenericRepository GenericRepository { get; }
        private IMapper Mapper { get; }

        public DetailsService(IGenericRepository genericRepository, IMapper mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
        }

        public Task<TEntityDetails> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntityDetails>> GetAllAsync()
        {
            var result = await GenericRepository.GetAllAsync<TEntity>();
            return Mapper.Map<IEnumerable<TEntityDetails>>(result);
        }

        public Task<IEnumerable<TEntityDetails>> FindAsync(Expression<Func<TEntityDetails, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
