using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Security;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        private readonly DetailsService<Domain.Entities.Admin, AdminDetailsModel> _detailsService;
        private readonly CreateService<Domain.Entities.Admin, AdminCreateModel> _createService;

        public AdminService(IGenericRepository genericRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;

            _detailsService = new DetailsService<Domain.Entities.Admin, AdminDetailsModel>
                (genericRepository, mapper);
            _createService = new CreateService<Domain.Entities.Admin, AdminCreateModel>
                (genericRepository, mapper);
        }

        public async Task<AdminDetailsModel> GetAsync(Guid id, params string[] includes)
        {
            return await _detailsService.GetAsync(id, includes);
        }

        public async Task<IEnumerable<AdminDetailsModel>> GetAllAsync(params string[] includes)
        {
            return await _detailsService.GetAllAsync(includes);
        }

        public async Task<Guid> AddAsync(AdminCreateModel entity)
        {
            var studentRoles = await _genericRepository.GetAllAsync<Role>();

            var studentRolesGuid = studentRoles.Where(r => r.Name == "Admin").Select(i => i.Id).ToList();

            var person = Domain.Entities.Person.Create(entity.FirstName, entity.LastName,
                entity.Email, entity.Gender, _passwordHasher.HashPassword(entity.Password), studentRolesGuid);

            var admin = Domain.Entities.Admin.Create(person);

            var result = await _genericRepository.AddAsync(admin);
            await _genericRepository.SaveAsync();

            return result;
        }

        public async Task<IEnumerable<Guid>> AddAsync(IEnumerable<AdminCreateModel> entities)
        {
            return await _createService.AddAsync(entities);
        }

        public async Task<Guid> UpdateAsync(Guid id, AdminCreateModel entity, params string[] includes)
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

        public async Task<AdminDetailsModel> GetAdminByPersonId(Guid id, params string[] includes)
        {
            var result = await _genericRepository.FindAsync<Domain.Entities.Admin>(a => a.PersonId == id, includes);
            return _mapper.Map<AdminDetailsModel>(result.FirstOrDefault());
        }
    }
}
