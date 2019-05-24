using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Application.Models;
using CampusManagement.Business.Article.Models;
using CampusManagement.Business.Authentication.Models;
using CampusManagement.Business.Hostel.Models;
using CampusManagement.Business.Person.Models;
using CampusManagement.Business.Security;
using CampusManagement.Business.Student.Models;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<AccessToken, TokenDetailsModel>()
                .ForMember(d=>d.AccessToken, opt => opt.MapFrom(s=>s.Token))
                .ForMember(d=>d.RefreshToken, opt=>opt.MapFrom(s=>s.RefreshToken.Token))
                .ForMember(d=>d.Expiration, opt=>opt.MapFrom(s=>s.Expiration))
                .ForMember(d=>d.Person, opt=>opt.MapFrom(s=>s.Person));

            CreateMap<Domain.Entities.Person, PersonDetailsModel>();

            CreateMap<Domain.Entities.Student, StudentDetailsModel>()
                .ForMember(d=>d.FirstName, opt => opt.MapFrom(s=>s.Person.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.Person.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(s=>s.Person.Email))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Person.Gender));

            CreateMap<StudentCreateModel, Domain.Entities.Student>();
/*
                //.BeforeMap((s, d) => d.Person = Domain.Entities.Person.Create(s.FirstName,s.LastName,s.Email,s.Gender,s.))
                .ForMember(d => d.Person.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.Person.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.Person.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.Person.Gender, opt => opt.MapFrom(s => s.Gender))
                .ForMember(d => d.Person.Password, opt => opt.MapFrom(s => s.Password)); //.AfterMap((s, d) => d.SetDefaults());#1#
*/

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

            CreateMap<Domain.Entities.Application, ApplicationDetailsModel>();
            CreateMap<ApplicationCreateModel, Domain.Entities.Application>()
                .ForMember(d => d.HostelPreferences, opt => opt.MapFrom(s => s.HostelPreferences))
                .AfterMap((s,d)=> d.SetApplicationForHostelPreferences(d.Id));

            CreateMap<Domain.Entities.HostelPreference, HostelPreferenceDetailsModel>();
            CreateMap<HostelPreferenceCreateModel, Domain.Entities.HostelPreference>();

            CreateMap<Domain.Entities.Hostel, HostelDetailsModel>();
            CreateMap<HostelCreateModel, Domain.Entities.Hostel>();

            //CreateMap(HostelPreferenceCreateModel, Domain.Entities.HostelPreference);
        }
    }
}
