using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Infrastructure.Repository
{
    public class AssignFileRepository : IAssignFileRepository
    {
        public readonly AppDbContext _context;
        public AssignFileRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<string>> GetFileNamesByUserIdZeroAsync()
        {
            return await _context.modelDimJson
                .Where(x => x.UserId == 0)
                .Select(x => x.FileName)
                .ToListAsync();
        }
        //public async Task AssignfileAsync(long userId, List<Guid> jsonIds)
        // {
        //     var records = await _context.modelDimJson
        //         .Where(x => jsonIds.Contains(x.Id) && x.UserId == 0)
        //         .ToListAsync();
        //     foreach (var record in records)
        //     {
        //         record.UserId = userId;
        //     }
        //     _context.modelDimJson.UpdateRange(records);
        //     await _context.SaveChangesAsync();
        // }
        public async Task<List<ModelDimJson>> GetFilesByJsonIdsAsync(List<Guid> jsonIds)
        {
            return await _context.modelDimJson
                .Where(f => jsonIds.Contains(f.Id))
                .ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task UserUpdateAsync(ModelDimJson modelDimJson)
        {
            _context.modelDimJson.Update(modelDimJson);
            _context.SaveChanges();
        }



    }
}
