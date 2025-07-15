using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimJsonRepository
    {

        Task<bool> ExistsByFileNameAsync(string fileName);
        Task InsertJsonRecordAsync(ModelDimJson model);

        Task SaveChangesAsync();


    }
}
