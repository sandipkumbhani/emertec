using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IAssignFilesService, AssignFilesService>();
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IGetUserNameByIdService, GetUserNameByIdService>();
            return services;
        }
    }
}
