using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Infrastructure.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Emertec.UI.Infrastructure.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddEfcoreInfrastrucureService(this IServiceCollection services)
        {
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuMasterRepository, MenuMasterRepository>();
            services.AddScoped<IMenuMappingRepository, MenuMappingRepository>();
            services.AddScoped<IShowTranscriptRepository, ShowTranscriptRepository>();
            services.AddScoped<IAssignFilesRepository, AssignFilesRepository>();
            services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
            services.AddScoped<IResetPasswordRepossitory, ResetPasswordRepossitory>();
            services.AddScoped<IGetUserNameByIdRepository, GetUserNameByIdRepository>();
            return services;
        }
    }
}
