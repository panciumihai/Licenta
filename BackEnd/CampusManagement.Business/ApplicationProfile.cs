using System;
using System.Linq.Expressions;
using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Article.Models;
using CampusManagement.Business.Person.Models;
using CampusManagement.Business.Student.Models;

namespace CampusManagement.Business
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Entities.Person, PersonDetailsModel>();

            CreateMap<Domain.Entities.Student, StudentDetailsModel>()
                .ForMember(d=>d.FirstName, opt => opt.MapFrom(s=>s.Person.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s=>s.Person.Email))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Person.Gender));

            CreateMap<StudentCreateModel, Domain.Entities.Student>(); //.AfterMap((s, d) => d.SetDefaults());

            CreateMap<Domain.Entities.Admin, AdminDetailsModel>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Person.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Person.Email))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Person.Gender));

            CreateMap<AdminCreateModel, Domain.Entities.Admin>(); //.AfterMap((s, d) => d.SetDefaults());

            CreateMap<Domain.Entities.Article, ArticleDetailsModel>()
                .ForMember(d => d.AuthorFirstName, opt => opt.MapFrom(s => s.Admin.Person.FirstName))
                .ForMember(d => d.AuthorLastName, opt => opt.MapFrom(s => s.Admin.Person.LastName));
            CreateMap<ArticleCreateModel, Domain.Entities.Article>();
        }
    }
}
