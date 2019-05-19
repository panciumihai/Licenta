using System.Collections.Generic;
using CampusManagement.Business.Admin;
using CampusManagement.Business.Admin.Models;
using CampusManagement.Business.Admin.Validations;
using CampusManagement.Business.Application;
using CampusManagement.Business.Article;
using CampusManagement.Business.Article.Models;
using CampusManagement.Business.Article.Validations;
using CampusManagement.Business.Authentication;
using CampusManagement.Business.File;
using CampusManagement.Business.Hostel;
using CampusManagement.Business.Person;
using CampusManagement.Business.Person.Models;
using CampusManagement.Business.Person.Validations;
using CampusManagement.Business.Security;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;
using CampusManagement.Business.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampusManagement.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            // FluentValidation services area //
            services.AddMvc().AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                fv.ImplicitlyValidateChildProperties = true;
            });

            services.AddTransient<IValidator<PersonCreateModel>, PersonCreateModelValidator>();

            services.AddTransient<IValidator<StudentCreateModel>, StudentCreateModelValidator>();
            services.AddTransient<IValidator<IEnumerable<StudentCreateModel>>,
                StudentCreateModelCollectionValidator>();

            services.AddTransient<IValidator<AdminCreateModel>, AdminCreateModelValidator>();
            services.AddTransient<IValidator<IEnumerable<AdminCreateModel>>,
                AdminCreateModelCollectionValidator>();

            services.AddTransient<IValidator<ArticleCreateModel>, ArticleCreateModelValidator>();

            // AutoMapper services area //
            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            // Business services area //
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IHostelService, HostelService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, TokenHandler>();
            services.AddSingleton<IFileService, FileService>();

            return services;
        }
    }
}
