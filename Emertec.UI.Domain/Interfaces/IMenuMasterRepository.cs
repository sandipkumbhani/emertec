using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IMenuMasterRepository
    {
        Task<List<ModelMenuMaster>> GetAllMenuAsync();
        Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster);
    }
}
