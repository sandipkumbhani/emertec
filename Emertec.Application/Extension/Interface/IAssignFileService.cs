using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IAssignFileService
    {
        Task<List<string>> GetFileNamesForUserZeroAsync();
        Task AssignUserToFilesAsync(int userId, List<Guid> jsonIds);
    }
}
