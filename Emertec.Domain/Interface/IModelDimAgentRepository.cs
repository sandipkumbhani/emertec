using MicroService_Template.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Interface
{
    public interface IModelDimAgentRepository
    {
        Task<bool> AgentExistsAsync(string firstName, string lastName);
        Task InsertAgentAsync(ModelDimAgent agent);
        Task SaveChangesAsync();
    }
}
