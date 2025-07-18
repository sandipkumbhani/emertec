
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IMenuMasterServices, MenuMasterServices>();
            services.AddScoped<IMenuMappingServices, MenuMappingService>();
            services.AddScoped<IShowTranscriptServices, ShowTranscriptServices>();
            return services;
        }
    }
}
