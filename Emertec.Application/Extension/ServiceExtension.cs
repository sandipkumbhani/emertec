using Microsoft.Extensions.DependencyInjection;
namespace Emertec.Application.Extension

{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection service)
        {
            // service.AddScoped<IClinicService, ClinicService>();

            return service;
        }
    }
}
