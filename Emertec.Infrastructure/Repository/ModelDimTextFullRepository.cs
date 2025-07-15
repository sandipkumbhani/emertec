using MicroService_Template.Domain.Extension.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroService_Template.Infrastructure.Repository
{
    public class ModelDimTextFullRepository : IModelDimTextFullRepository
    {
        private readonly AppDbContext _context;

        public ModelDimTextFullRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertAsync(ModelDimTextFull textFull)
        {
            await _context.modelDimTextFull.AddAsync(textFull);
        }

        public async Task<ModelDimJson?> GetByJsonidAsync(Guid guid)
        {
            return await _context.modelDimJson.FirstOrDefaultAsync(x => x.Id == guid);
        }
        public async Task<ModelDimTextFull?> GetByJsonGuidAsync(Guid jsonGuid)
        {
            return await _context.modelDimTextFull.FirstOrDefaultAsync(x => x.JsonGuid == jsonGuid);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
