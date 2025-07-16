using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
   public interface IModelCreateUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
       Task<ModelUsers> AddUserAsync(ModelUsers user);
       Task<List<ModelUsers>> GetAllUsersAsync();
        ModelUsers GetUserById(int id);
       Task DeleteAsync(ModelUsers modelUsers);
       Task UserUpdateAsync(ModelUsers modelUsers);
    }
}
