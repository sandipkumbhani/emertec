using Microsoft.Extensions.DependencyInjection;

namespace Emertec.Infrastructure.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfrastrucureService(this IServiceCollection services)
        {
           // services.AddScoped<IClinicRepository, ClinicRepository>();
            
            return services;
        }
    }
}
