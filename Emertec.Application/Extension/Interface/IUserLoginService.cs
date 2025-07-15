using MicroService_Template.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface IUserLoginService
    {
        //Task<string> LoginAsync(LoginUserDTO dto);
        Task<LoginUserDTO?> LoginAsync(string email, string password);
        Task<List<UserDTO>> GetAllUsersAsync();
    }
}
