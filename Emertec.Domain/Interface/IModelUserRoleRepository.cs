using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelUserRoleRepository
    {
        Task<ModelUserRole> AddUserRoleAsync(ModelUserRole modelUserRole);
        Task<List<ModelUserRole>> GetAllUsersRole();
        Task UserRoleUpdateAsync(ModelUserRole modelUserRole);
        Task DeleteRoleAsync(ModelUserRole modelUserRole);
        Task<ModelUserRole> GetUserRoleById(int roleid);
    }
}
