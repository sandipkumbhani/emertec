using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelUserMenuMappingRepository
    {
        Task<List<ModelUserMenuMapping>> AddMenuMappingAsync(IList<ModelUserMenuMapping> modelUserMenuMapping);
        Task<List<ModelUserMenuMapping>> GetAllMenuMapping();
        Task<ModelUserMenuMapping?> GetMenuMappingById(int menuMasterid);
        Task DeleteMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task UpdatMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<List<ModelUserMenuMapping>> GetAllActiveMappingsByUserIdAsync(int userId);

    }
}
