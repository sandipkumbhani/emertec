
using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
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
        public async Task<ModelUsers?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUsersByIdAsync(userId);
        }
        public async Task<string> AddUserAsync(ModelUsers user)
        {
            return await _userRepository.AddUserAsync(user);
        }
        public async Task<string> UpdateUserAsync(ModelUsers model)
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
    }

}
