using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Generics;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;

namespace CampusManagement.Business
{
    public class StudentService : 
        IStudentService
    {
        private IGenericRepository GenericRepository { get; }
        private IMapper Mapper { get; }
        private DetailsService<Domain.Entities.Student, StudentDetailsModel> DetailsService { get; }
        private CreateService<Domain.Entities.Student, StudentCreateModel> CreateService { get; }

        public StudentService(IGenericRepository genericRepository, IMapper mapper)
          //  :base(genericRepository, mapper)
        {
            GenericRepository = genericRepository;
            Mapper = mapper;
            DetailsService = new DetailsService<Domain.Entities.Student, StudentDetailsModel>
                (genericRepository, mapper);
            CreateService = new CreateService<Domain.Entities.Student, StudentCreateModel>
                (genericRepository, mapper);
        }

        public async Task<StudentDetailsModel> GetAsync(Guid id)
        {
            return await DetailsService.GetAsync(id);
        }

        public async Task<IEnumerable<StudentDetailsModel>> GetAllAsync()
        {
            return await DetailsService.GetAllAsync();
        }

        public async Task<IEnumerable<StudentDetailsModel>> FindAsync(Expression<Func<StudentDetailsModel, bool>> expression)
        {
            return await DetailsService.FindAsync(expression);
        }

        public async Task<Guid> AddAsync(StudentCreateModel entity)
        {
            return await CreateService.AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<StudentCreateModel> entities)
        {
            await CreateService.AddAsync(entities);
        }

        public async Task UpdateAsync(StudentCreateModel entity)
        {
            await CreateService.UpdateAsync(entity);
        }

        public async Task DeleteAsync(StudentCreateModel entity)
        {
            await CreateService.DeleteAsync(entity);
        }

        public async Task DeleteAsync(IEnumerable<StudentCreateModel> entities)
        {
            await CreateService.DeleteAsync(entities);
        }
    }
}
