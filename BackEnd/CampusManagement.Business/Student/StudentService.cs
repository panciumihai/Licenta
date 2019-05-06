using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Security;
using CampusManagement.Business.Student.Models;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business.Student
{
    public class StudentService : 
        IStudentService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        private readonly DetailsService<Domain.Entities.Student, StudentDetailsModel> _detailsService;
        private readonly CreateService<Domain.Entities.Student, StudentCreateModel> _createService;

        public StudentService(IGenericRepository genericRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;

            _detailsService = new DetailsService<Domain.Entities.Student, StudentDetailsModel>
                (genericRepository, mapper);
            _createService = new CreateService<Domain.Entities.Student, StudentCreateModel>
                (genericRepository, mapper);
        }

        public async Task<StudentDetailsModel> GetAsync(Guid id, params string[] includes)
        {
            //var result =
             //   await _genericRepository.FindAsync<Domain.Entities.Student>(s => s.Available && s.Id == id, "Person");
            return await _detailsService.GetAsync(id, includes);
        }

        public async Task<IEnumerable<StudentDetailsModel>> GetAllAsync(params string[] includes)
        {
            var result= await _genericRepository.GetAllAsync<Domain.Entities.Student>(includes);

            return _mapper.Map<IEnumerable<StudentDetailsModel>>(result);
            //return await _detailsService.GetAllAsync(pagination, includes);
        }

        public async Task<Guid> AddAsync(StudentCreateModel entity)
        {
            var studentRoles = await _genericRepository.GetAllAsync<Role>();

            var studentRolesGuid = studentRoles.Where(r => r.Name == "Student").Select(i=>i.Id).ToList();

            var person = Entities.Person.Create(entity.FirstName, entity.LastName, 
                entity.Email, entity.Gender, _passwordHasher.HashPassword(entity.Password), studentRolesGuid);

            var student = Domain.Entities.Student.Create(person, entity.Year);

            var result = await _genericRepository.AddAsync(student);
            await _genericRepository.SaveAsync();

            return result;
        }

        public async Task<IEnumerable<Guid>> AddAsync(IEnumerable<StudentCreateModel> entities)
        {
            return await _createService.AddAsync(entities);
        }

        public async Task<Guid> UpdateAsync(Guid id, StudentCreateModel entity)
        {
            return await _createService.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _createService.DeleteAsync(id);
        }

        public async Task DeleteAsync(IEnumerable<Guid> idList)
        {
            await _createService.DeleteAsync(idList);
        }
    }
}
