using Emertac.UI.Application.Interface;
using Emertac.UI.Domain.DTO;
using Emertac.UI.Domain.Interface;

namespace Emertac.UI.Application.Service
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ILoginUserRepository _loginUserRepository;

        public LoginUserService(ILoginUserRepository loginUserRepository)
        {
            _loginUserRepository = loginUserRepository;
        }
        public async Task<bool> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO == null)
            {
               Console.WriteLine("Login user data cannot be null.");
            }
            return await _loginUserRepository.LoginUserAsync(loginUserDTO);
        }
    }
}
