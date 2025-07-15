using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface IModelDimTextFullRepository
    {
        Task<ModelDimJson?> GetByJsonidAsync(Guid guid);
        Task InsertAsync(ModelDimTextFull textFull);
       Task<ModelDimTextFull?> GetByJsonGuidAsync(Guid jsonGuid);
        Task SaveChangesAsync();
    }
}
