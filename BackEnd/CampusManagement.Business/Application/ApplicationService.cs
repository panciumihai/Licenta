using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Application.Models;
using CampusManagement.Business.Generics;

namespace CampusManagement.Business.Application
{
    public class ApplicationService : IApplicationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        private readonly DetailsService<Domain.Entities.Application, ApplicationDetailsModel> _detailsService;
        private readonly CreateService<Domain.Entities.Application, ApplicationCreateModel> _createService;

        public ApplicationService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;

            _detailsService = new DetailsService<Domain.Entities.Application, ApplicationDetailsModel>
                (genericRepository, mapper);
            _createService = new CreateService<Domain.Entities.Application, ApplicationCreateModel>
                (genericRepository, mapper);
        }

        public async Task<ApplicationDetailsModel> GetAsync(Guid id, params string[] includes)
        {
            return await _detailsService.GetAsync(id, includes);
        }

        public async Task<IEnumerable<ApplicationDetailsModel>> GetAllAsync(params string[] includes)
        {
            return await _detailsService.GetAllAsync(includes);
        }

        public async Task<Guid> AddAsync(ApplicationCreateModel entity)
        {


            return await _createService.AddAsync(entity);
        }

        public async Task<IEnumerable<Guid>> AddAsync(IEnumerable<ApplicationCreateModel> entities)
        {
            return await _createService.AddAsync(entities);
        }

        public async Task<Guid> UpdateAsync(Guid id, ApplicationCreateModel entity)
        {
            return await _createService.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _createService.DeleteAsync(id);
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            await _createService.DeleteAsync(ids);
        }
    }
}
