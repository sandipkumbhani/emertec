using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MicroService_Template.Infrastructure.Repository
{
    public class ModelDimJsonRepository : IModelDimJsonRepository
    {
        private readonly AppDbContext _context;

        public ModelDimJsonRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task InsertJsonRecordAsync(ModelDimJson modeldimjson)
        {
            _context.modelDimJson.Add(modeldimjson);
          
        }
        public async Task<bool> ExistsByFileNameAsync(string fileName)
        {
            return await _context.modelDimJson
                .AnyAsync(x => x.FileName == fileName);
        }

        //public async task<modeldimjson?> getbydapperguidasync(guid guid)
        //{
        //    return await _context.modeldimjson.firstordefaultasync(x => x.dapperguid == guid);
        //}
        //public async Task<ModelDimJson?> GetByJsonidAsync(Guid guid)
        //{
        //    return await _context.modelDimJson.FirstOrDefaultAsync(x => x.Id == guid);
        //}

        //public async Task UpdateAsync(ModelDimJson model)
        //{
        //    _context.modelDimJson.Update(model);
        //    await _context.SaveChangesAsync();
        //}
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
