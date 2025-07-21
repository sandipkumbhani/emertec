using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<ModelUsers> GetUsersByIdAsync(int? id);
        Task<string> AddUserAsync(ModelUsers user);
        Task<string> UpdateUserAsync(ModelUsers user);
        Task<string> DeleteUserAsync(int id);
        //UserRole
        Task<List<ModelUserRole>> GetAllUserRoleAsync();
    }
}
