using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Infrastructure.Repository
{
    public class ModelDimAgentRepository : IModelDimAgentRepository
    {

        private readonly AppDbContext _context;

        public ModelDimAgentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AgentExistsAsync(string firstName, string lastName)
        {
            return await _context.modelDimAgent
                .AnyAsync(x => x.FirstName == firstName && x.LastName == lastName);
        }

        public async Task InsertAgentAsync(ModelDimAgent agent)
        {
            await _context.modelDimAgent.AddAsync(agent);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
