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
    public class ModelDimCampaignRepository : IModelDimCampaignRepository
    {
        private readonly AppDbContext _context;

        public ModelDimCampaignRepository(AppDbContext context)
        {
            _context = context;
        }
       
        public async Task<ModelDimCampaign?> GetByNameAsync(string campaignName)
        {
            return await _context.modelDimCampaign
                .FirstOrDefaultAsync(c => c.Name == campaignName);
        }
        public async Task<Guid?> GetCompanyIdByCampaignNameAsync(string campaignName)
        {
            return await _context.modelDimCampaign
                                 .Where(c => c.Name == campaignName)
                                 .Select(c => c.CompanyId)
                                 .FirstOrDefaultAsync();
        }

        public async Task campaignInsertAsync(ModelDimCampaign campaign)
        {
            await _context.modelDimCampaign.AddAsync(campaign);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
       
    }
}