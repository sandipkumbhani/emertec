using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<ModelUsers> GetUsersByIdAsync(int? id);
        Task<string> AddUserAsync(ModelUsers user);
        Task<string> UpdateUserAsync(ModelUsers user);
        //UserRole
        Task<List<ModelUserRole>> GetAllUserRoleAsync();
    }
}
