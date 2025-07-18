using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<string> AddUserAsync(ModelUsers user);

        //UserRole
        Task<List<ModelUserRole>> GetAllUserRoleAsync();
        Task<ModelUsers?> GetUserByIdAsync(int userId);
        Task<string> UpdateUserAsync(ModelUsers model);
        Task<string> DeleteUserAsync(int userId);
    }
}
