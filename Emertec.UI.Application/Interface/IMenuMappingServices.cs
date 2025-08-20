using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IMenuMappingServices
    {
        Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync();
        Task<ModelUserMenuMapping> AddMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<ModelUserMenuMapping> GetMenuMappingByIdAsync(int userId);
        Task<string> DeleteMenuMappingAsync(int userId);
        Task<string> UpdateMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
    }
}
