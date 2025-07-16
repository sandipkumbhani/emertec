
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IModelCreateUserService
    {
        Task<ModelUsers> CreateUserAsync(ModelUsers modelUsers);
        Task<List<ModelUsers>> GetAllUsersAsync();
         Task DeleteUserById(int id);
        Task<ModelUsers> UpdateUserAsync(int userid, ModelUsers modelUsers);
    }
}
