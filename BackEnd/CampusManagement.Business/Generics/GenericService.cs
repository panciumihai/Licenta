using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Entities;

namespace CampusManagement.Business
{
    public class GenericService<TEntity, TEntityModelView> : IGenericService<TEntityModelView> 
        where TEntity : Entity
        where TEntityModelView : class
    {
        public IGenericRepository GenericRepository { get; set; }
        public IMapper Mapper { get; set; }

        public GenericService(IGenericRepository genericRepository, IMapper mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
        }

        public Task<TEntityModelView> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntityModelView>> GetAllAsync()
        {
            var result = await GenericRepository.GetAllAsync<TEntity>();
            return Mapper.Map<IEnumerable<TEntityModelView>>(result);
        }

        public Task<IEnumerable<TEntityModelView>> FindAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
