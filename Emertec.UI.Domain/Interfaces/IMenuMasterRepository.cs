using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IMenuMasterRepository
    {
        Task<List<ModelMenuMaster>> GetAllMenuAsync();
        Task<ModelMenuMaster> GetMenuByIdAsync(int? id);
        Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster);
        Task<string> UpdateMenuAsync(ModelMenuMaster menuMaster);
        Task<string> DeletemenuAsync(int id);
    }
}
