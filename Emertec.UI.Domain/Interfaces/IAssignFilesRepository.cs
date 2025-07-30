using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IAssignFilesRepository
    {
        Task<List<ModelUsers>> GetAllUsersAsync();
        Task<List<string>> GetFileNamesForUserZeroAsync();
        Task<bool> AssignUserToFilesAsync(int userId, List<Guid> jsonIds);
    }
}
