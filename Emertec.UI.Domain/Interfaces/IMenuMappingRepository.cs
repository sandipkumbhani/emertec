using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IMenuMappingRepository
    {
        Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync();
        Task<string> AddMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<string> UpdateMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping);
        Task<ModelUserMenuMapping> GetMenuMappingByIdAsync(int? userId);
        Task<string> DeleteMenuMappingAsync(int userId);
    }
}
