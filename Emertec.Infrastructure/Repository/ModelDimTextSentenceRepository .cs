using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroService_Template.Infrastructure.Repository
{
    public class IModelDimTextWordRepository : IModelDimTextSentenceRepository
    {
        private readonly AppDbContext _context;

        public IModelDimTextWordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelDimJson?> GetByDapperGuidAsync(Guid guid)
        {
            return await _context.modelDimJson.FirstOrDefaultAsync(x => x.Id == guid);
        }

        public async Task InsertAsync(ModelDimTextSentence sentence)
        {
            await _context.modelDimTextSentence.AddAsync(sentence);
        }
        public async Task<bool> ExistsByJsonGuidAsync(Guid jsonGuid)
        {
            return await _context.modelDimTextSentence.AnyAsync(s => s.JsonGuid == jsonGuid);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
