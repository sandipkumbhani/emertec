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
    public class ModelDimCompanyRepository : IModelDimCompanyRepository
    {

        private readonly AppDbContext _context;

        public ModelDimCompanyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelDimCompany> GetByNameAsync(string name)
        {
            return await _context.modelDimCompany.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<ModelDimCompany?> GetCompanyIdByNameAsync(string name)
        {
            var trimmedName = name.Trim().ToLower();
            return await _context.modelDimCompany
                                 .FirstOrDefaultAsync(c => c.Name.ToLower() == trimmedName);
        }
        public async Task companyInsertAsync(ModelDimCompany company)
        {
            await _context.modelDimCompany.AddAsync(company);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
      

}
}
