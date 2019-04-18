using System;
using CampusManagement.Business.Models;
using CampusManagement.Business.Student.Models;
using CampusManagement.Entities;

namespace CampusManagement.Business
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Entities.Person, PersonDetailsModel>();
            CreateMap<Domain.Entities.Student, StudentDetailsModel>();
            CreateMap<StudentCreateModel, Domain.Entities.Student>().AfterMap((s, d) => d.SetDefaults());
        }
    }
}
