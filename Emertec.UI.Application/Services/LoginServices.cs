using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Domain.Models;

namespace Emertec.UI.Application.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginRepository _loginRepository;
        public LoginServices(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public async Task<string> Login(LoginViewModel model)
        {
            return await _loginRepository.CreateUserLoginAsync(model);
        }
    }
}
