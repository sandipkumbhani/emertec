
using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Interface
{
    public interface IUserServices
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<string> AddUserAsync(ModelUsers user);

        //UserRole

        Task<List<ModelUserRole>> GetAllUserRoleAsync();
    }
}

