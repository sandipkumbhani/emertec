using Emertac.UI.Application.Interface;
using Emertac.UI.Domain.DTO;
using Emertac.UI.Domain.Interface;

namespace Emertac.UI.Application.Service
{
    public class CreateUserService : ICreateUserService
    {
        private readonly ICreateUserRepository _createUserRepository;
        public CreateUserService(ICreateUserRepository createUserRepository)
        {
            _createUserRepository = createUserRepository;
        }
        public async Task<bool> AddUser(CreateUserDTO createUserDTO)
        {
            return await _createUserRepository.RegisterUserAsync(createUserDTO);
        }


    }
}
