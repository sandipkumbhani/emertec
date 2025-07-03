using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimJsonRepository
    {
        Task<ModelDimJson?> GetByJsonidAsync(Guid guid);
        Task InsertJsonRecordAsync(ModelDimJson model);
        Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid);
        Task UpdateAsync(ModelDimJson model);
        Task SaveChangesAsync();
        

    }
}
