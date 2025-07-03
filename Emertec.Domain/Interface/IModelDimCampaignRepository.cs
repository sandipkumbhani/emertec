using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public  interface IModelDimCampaignRepository
    {
        Task<ModelDimCampaign?> GetByNameAsync(string campaignName);
        Task campaignInsertAsync(ModelDimCampaign campaign);
        Task<Guid?> GetCompanyIdByCampaignNameAsync(string campaignName);
        Task SaveChangesAsync();
    }
}
