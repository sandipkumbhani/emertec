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
    public class ModelDimTextWordRepositorycs : IModelDimTextWordRepositorycs
    {
        private readonly AppDbContext _context;

        public ModelDimTextWordRepositorycs(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelDimJson?> GetByGuidAsync(Guid guid)
        {
            return await _context.modelDimJson.FirstOrDefaultAsync(x => x.Id == guid);
        }
        public async Task<ModelDimTextSentence?> GetbyTextSentenceIdAsync(Guid guid)
        {
            return await _context.modelDimTextSentence.FirstOrDefaultAsync(x => x.Id == guid);
        }
        public async Task InsertAsync(ModelDimWord word)
        {
            await _context.modelDimWord.AddAsync(word);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
