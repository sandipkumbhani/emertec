using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IMenuMasterServices
    {
        Task<List<ModelMenuMaster>> GetAllMenuMasterAsync();
        Task<ModelMenuMaster?> GetMenuByIdAsync(int menuId);
        Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster);
        Task<string> UpdateMenuAsync(ModelMenuMaster modelMenuMaster);
        Task<string> DeleteMenuAsync(int menuId);
    }
}
