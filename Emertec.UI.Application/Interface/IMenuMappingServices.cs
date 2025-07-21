
using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Interface
{
    public interface IMenuMappingServices
    {
        Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync();
        Task<string> AddMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<ModelUserMenuMapping> GetMenuMappingByIdAsync(int userId);
        Task<string> DeleteMenuMappingAsync(int userId);
        Task<string> UpdateMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
    }
}
