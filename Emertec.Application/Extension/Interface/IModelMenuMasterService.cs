using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IModelMenuMasterService
    {
        Task<ModelMenuMaster> CreateMenuMasterAsync(ModelMenuMaster modelMenuMaster);
        Task<List<ModelMenuMaster>> GetModelMenuMastersAsync();
       Task DeleteMenuById(int id);
        Task<ModelMenuMaster> UpdateMenuAsync(int menuid, ModelMenuMaster modelMenuMaster);
        Task<ModelMenuMaster> GetMenuMsaterById(int id);
        Task<List<ModelMenuMaster>> GetMenusByUserIdAsync(long userId);
    }
}
