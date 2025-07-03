using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimTextSentenceRepository
    {
        Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid);
        Task InsertAsync(ModelDimTextSentence sentence);
        Task<bool> ExistsByJsonGuidAsync(Guid jsonGuid);
        Task SaveChangesAsync();
    }
}
