﻿using System;
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
            //var studentRoles = await _genericRepository.GetAllAsync<Role>();

            //var studentRolesGuid = studentRoles.Where(r => r.Name == "Student").Select(i=>i.Id).ToList();

            var studentRolesGuid = _genericRepository.FindAsync<Role>(r => r.Name == "Student").Result.Select(p => p.Id)
                .ToList();

            var person = Domain.Entities.Person.Create(entity.FirstName, entity.LastName, 
                entity.Email, entity.Gender, _passwordHasher.HashPassword(entity.Password), studentRolesGuid);

            var student = Domain.Entities.Student.Create(person, entity.Year, entity.Cnp,
                entity.Nationality,entity.Score,entity.SecondScore);

            var result = await _genericRepository.AddAsync(student);
            await _genericRepository.SaveAsync();

            return result;
        }

        public async Task<IEnumerable<Guid>> AddAsync(IEnumerable<StudentCreateModel> entities)
        {
            IEnumerable<Guid> ids = new List<Guid>();

            foreach (var studentCreateModel in entities)
                ids.Append(await AddAsync(studentCreateModel));

            return ids;
        }

        public async Task<Guid> UpdateAsync(Guid id, StudentCreateModel entity, params string[] includes)
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

        public async Task<StudentDetailsModel> GetStudentByPersonId(Guid id, params string[] includes)
        {
            var result = await _genericRepository.FindAsync<Domain.Entities.Student>(s=>s.PersonId == id, includes);

            return _mapper.Map<StudentDetailsModel>(result.FirstOrDefault());
        }

        public async Task<Guid> Confirmation(StudentConfirmationModel studentConfirmationModel)
        {
            var student = await _genericRepository.GetAsync<Domain.Entities.Student>(studentConfirmationModel.Id);
            student.Confirmation(studentConfirmationModel.Confirmed, studentConfirmationModel.StudentsGroupId);

            var resullt = await _genericRepository.UpdateAsync(student);
            await _genericRepository.SaveAsync();

            return resullt;
        }

        public async Task<IEnumerable<Guid>> SetStudentsGroup(IEnumerable<StudentConfirmationModel> studentConfirmationModels)
        {
            var results = new List<Guid>();
            foreach (var studentConfirmationModel in studentConfirmationModels)
            {
                var student = await _genericRepository.GetAsync<Domain.Entities.Student>(studentConfirmationModel.Id);
                student.Confirmation(studentConfirmationModel.Confirmed, studentConfirmationModel.StudentsGroupId);
                results.Add(await _genericRepository.UpdateAsync(student));
            }

            await _genericRepository.SaveAsync();

            return results;
        }

        public async Task<IEnumerable<StudentConfirmedDetailsModel>> GetAllConfirmedAsync(params string[] includes)
        {
            var results = await _genericRepository.FindAsync<Domain.Entities.Student>(s => s.Confirmed, "StudentsGroup.HostelStatus.Hostel", "Person");
            return _mapper.Map<IEnumerable<StudentConfirmedDetailsModel>>(results);
        }

        public async Task<IEnumerable<StudentConfirmedDetailsModel>> GetAllUnconfirmedAsync(params string[] includes)
        {
            var results = await _genericRepository.FindAsync<Domain.Entities.Student>(s => s.Confirmed == false && s.StudentsGroupId != null, "StudentsGroup.HostelStatus.Hostel", "Person");
            return _mapper.Map<IEnumerable<StudentConfirmedDetailsModel>>(results);
        }

        public async Task<StudentConfirmedDetailsModel> GetWithHostelById(Guid id, params string[] includes)
        {
            var results = await _genericRepository.FindAsync<Domain.Entities.Student>(s => s.Id == id, "StudentsGroup.HostelStatus.Hostel", "Person");
            return _mapper.Map<StudentConfirmedDetailsModel>(results.FirstOrDefault());
        }
    }
}
