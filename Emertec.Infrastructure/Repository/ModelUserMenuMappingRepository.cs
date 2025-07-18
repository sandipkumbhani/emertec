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
    public class ModelUserMenuMappingRepository : IModelUserMenuMappingRepository
    {
        private readonly AppDbContext _context;
        public ModelUserMenuMappingRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ModelUserMenuMapping>> AddMenuMappingAsync(IList<ModelUserMenuMapping> modelUserMenuMappingList)
        {
            _context.modelUserMenuMappings.AddRange(modelUserMenuMappingList);
            await _context.SaveChangesAsync();
            return await _context.modelUserMenuMappings
                .Include(m => m.User)
                .Include(m => m.Menu)
                 .Where(s => s.UserId == modelUserMenuMappingList.First().UserId)
        .ToListAsync();
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMapping()
        {
            return await _context.modelUserMenuMappings
         .Include(x => x.User)
         .Include(x => x.Menu)
         .Where(u => u.IsActive)
         .ToListAsync();
        }
        public async Task<ModelUserMenuMapping?> GetMenuMappingById(int menuMasterid)
        {
            return await _context.modelUserMenuMappings
                .Include(x => x.User)
                .Include(x => x.Menu)
                .FirstOrDefaultAsync(e => e.UserMenuMappingId == menuMasterid);
        }

        public async Task DeleteMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            _context.modelUserMenuMappings.Remove(modelUserMenuMapping);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            _context.modelUserMenuMappings.Update(modelUserMenuMapping);
            _context.SaveChanges();
        }


    }
}
