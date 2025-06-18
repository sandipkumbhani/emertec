using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
