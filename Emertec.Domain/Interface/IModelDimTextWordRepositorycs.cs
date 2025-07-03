using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimTextWordRepositorycs
    {

        Task<ModelDimJson?> GetByGuidAsync(Guid guid);

        Task<ModelDimTextSentence?> GetbyTextSentenceIdAsync(Guid guid);

        Task InsertAsync(ModelDimWord word);
        Task SaveChangesAsync();
    }
}
