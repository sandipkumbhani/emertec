using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelMenuMasterRepository
    {
        Task<ModelMenuMaster> AddMenuMasterAsync(ModelMenuMaster modelMenuMaster);
        Task<List<ModelMenuMaster>> GetAllMenuAsync();
        ModelMenuMaster GetMenuById(int menuid);
        Task DeleteMenuAsync(ModelMenuMaster modelMenuMaster);
        Task UpdatMenuAsync(ModelMenuMaster modelMenuMaster);
    }
}
