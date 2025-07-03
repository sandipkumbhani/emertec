using MicroService_Template.Domain.Model;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimAgentRepository
    {
        Task<bool> AgentExistsAsync(string firstName, string lastName);
        Task InsertAgentAsync(ModelDimAgent agent);
        Task SaveChangesAsync();
    }
}
