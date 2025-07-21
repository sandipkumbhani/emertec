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
            try
            {
                if (modelUserMenuMappingList == null || !modelUserMenuMappingList.Any())
                {
                    throw new ArgumentException("Mapping list is null or empty");
                }
                _context.modelUserMenuMappings.AddRange(modelUserMenuMappingList);
                await _context.SaveChangesAsync();
                long userId = modelUserMenuMappingList.First().UserId;
                return await _context.modelUserMenuMappings.Where(m => m.UserId == userId && m.IsActive).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddMenuMappingAsync: " + ex.Message, ex);
            }
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMapping()
        {
            return await _context.modelUserMenuMappings
         .Include(x => x.User)
         .Include(x => x.Menu)
         .Where(u => u.IsActive)
         .ToListAsync();
        }
        public async Task<ModelUserMenuMapping?> GetMenuMappingById(int UserId)
        {
            var userMenuMappings = await _context.modelUserMenuMappings
       .Where(x => x.UserId == UserId && x.IsActive)
       .ToListAsync();

            if (!userMenuMappings.Any()) return null;

            var firstRecord = userMenuMappings.First();

            // Combine all MenuIds as string for UI display
            firstRecord.MenuIds = string.Join(",", userMenuMappings.Select(x => x.MenuId));

            return firstRecord;
        }
        public async Task<List<ModelUserMenuMapping>> GetAllActiveMappingsByUserIdAsync(int userId)
        {
            return await _context.modelUserMenuMappings
                .Where(x => x.UserId == userId && x.IsActive)
                .ToListAsync();
        }
        public async Task DeleteMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            var existingMenu = await _context.modelUserMenuMappings
        .Where(x => x.UserId == modelUserMenuMapping.UserId && x.IsActive)
        .ToListAsync();

            if (!existingMenu.Any()) return;

            foreach (var mapping in existingMenu)
            {
                mapping.IsActive = false;
                mapping.UpdateBy = 1;
                mapping.UpdateDate = DateTime.UtcNow;
            }
            _context.modelUserMenuMappings.UpdateRange(existingMenu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            _context.modelUserMenuMappings.Update(modelUserMenuMapping);
            _context.SaveChanges();
        }


    }
}
