using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<string> AddUserAsync(ModelUsers user);
    }
}
