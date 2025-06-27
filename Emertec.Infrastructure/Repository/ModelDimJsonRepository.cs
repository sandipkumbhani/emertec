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
    public class ModelDimJsonRepository : IModelDimJsonRepository
    {
        private readonly AppDbContext _context;

        public ModelDimJsonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertJsonRecordAsync(ModelDimJson model)
        {
            _context.modelDimJson.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid)
        {
            return await _context.modelDimJson.FirstOrDefaultAsync(x => x.DapperGuid == guid);
        }

        public async Task UpdateAsync(ModelDimJson model)
        {
            _context.modelDimJson.Update(model);


        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
