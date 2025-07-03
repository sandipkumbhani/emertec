using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimCompanyRepository
    {
        Task<ModelDimCompany> GetByNameAsync(string name);
        Task companyInsertAsync(ModelDimCompany company);
        Task<ModelDimCompany?> GetCompanyIdByNameAsync(string name);

        Task SaveChangesAsync();
    }
}
