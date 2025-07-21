using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IUserServices
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<ModelUsers?> GetUserByIdAsync(int userId);
        Task<string> AddUserAsync(ModelUsers user);
        Task<string> UpdateUserAsync(ModelUsers model);
        Task<string> Deleteuserasync(int userid);
        //UserRole
        Task<List<ModelUserRole>> GetAllUserRoleAsync();
    }
}

