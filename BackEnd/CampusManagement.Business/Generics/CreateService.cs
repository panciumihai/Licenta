using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Entities;

namespace CampusManagement.Business.Generics
{
    public class CreateService<TEntity, TCreateEntity> : ICreateService<TCreateEntity>
        where TEntity : Entity
        where TCreateEntity : class
    {
        private IGenericRepository GenericRepository { get; }
        private IMapper Mapper { get; }

        public CreateService(IGenericRepository genericRepository, IMapper mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
        }


        public async Task<Guid> AddAsync(TCreateEntity entity)
        {
            var entityData = Mapper.Map<TEntity>(entity);
            await GenericRepository.AddAsync(entityData);
            await GenericRepository.SaveAsync();
            return entityData.Id;
        }

        public async Task AddAsync(IEnumerable<TCreateEntity> entities)
        {
            var entitiesData = Mapper.Map<TEntity>(entities);
            await GenericRepository.AddAsync(entitiesData);
            await GenericRepository.SaveAsync();
        }

        public async Task UpdateAsync(TCreateEntity entity)
        {
            var entityData = Mapper.Map<TEntity>(entity);
            await GenericRepository.UpdateAsync(entityData);
            await GenericRepository.SaveAsync();
        }

        public async Task DeleteAsync(TCreateEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IEnumerable<TCreateEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
