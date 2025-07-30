using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IAssignFileRepository
    {
        Task<List<string>> GetFileNamesByUserIdZeroAsync();
        //Task AssignfileAsync(long userId, List<Guid> jsonIds);
        Task<List<ModelDimJson>> GetFilesByJsonIdsAsync(List<Guid> jsonIds);
        Task<List<Guid>> GetjsonidByFileNmaeAsync(List<string> fileNames);
        Task SaveChangesAsync();
    }
}
