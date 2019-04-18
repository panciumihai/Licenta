using CampusManagement.Business.Generics;
using CampusManagement.Business.Student;
using CampusManagement.Business.Student.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace CampusManagement.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddMvc().AddFluentValidation(fv => {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });


            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new ApplicationProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<IValidator<StudentCreateModel>, StudentCreateModelValidator>();

            //services.AddScoped(typeof(IDetailsService<>), typeof(DetailsService<>));
            services.AddScoped<IStudentService, StudentService>();
            

            return services;
        }
    }
}
