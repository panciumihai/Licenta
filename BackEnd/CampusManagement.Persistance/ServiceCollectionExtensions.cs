using CampusManagement.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CampusManagement.Persistance
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CampusManagementContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IGenericRepository>(provider => provider.GetService<CampusManagementContext>());
            return services;
        }
    }
}
