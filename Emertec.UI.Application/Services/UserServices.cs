using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<ModelUsers>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
        public async Task<ModelUsers?> GetUserByIdAsync(long userId)
        {
            return await _userRepository.GetUsersByIdAsync(userId);
        }
        public async Task<ModelUsers> AddUserAsync(ModelUsers user)
        {
            return await _userRepository.AddUserAsync(user);
        }
        public async Task<ModelUsers> UpdateUserAsync(ModelUsers model)
        {
            return await _userRepository.UpdateUserAsync(model);
        }
        public async Task<string> Deleteuserasync(int userid)
        {
            return await _userRepository.DeleteUserAsync(userid);
        }
        //UsrRole
        public async Task<List<ModelUserRole>> GetAllUserRoleAsync()
        {
            return await _userRepository.GetAllUserRoleAsync();
        }
        public async Task<ModelUserRole> GetRoleNameByIdAsync(long? id)
        {
            return await _userRepository.GetRoleNameByIdAsync(id);
        }
    }
}
