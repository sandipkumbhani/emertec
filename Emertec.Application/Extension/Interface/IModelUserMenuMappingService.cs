using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IModelUserMenuMappingService
    {
        Task<List<ModelUserMenuMapping>> CreateMenuMasterMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync();
        Task<ModelUserMenuMapping> UpdateMenuMappingAsync(int UserMenuMappingId, ModelUserMenuMapping modelUserMenuMapping);
        Task DeleteMenuMappingById(int id);
        Task<ModelUserMenuMapping> GetMenuMappingDetailsById(int Menuid);
    }
}
