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
    public class ModelMenuMasterRepository : IModelMenuMasterRepository
    {
        private readonly AppDbContext _context;
        public ModelMenuMasterRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelMenuMaster> AddMenuMasterAsync(ModelMenuMaster modelMenuMaster)
        {
            _context.modelMenuMasters.Add(modelMenuMaster);
            await _context.SaveChangesAsync();
            return modelMenuMaster;
        }
        public async Task<List<ModelMenuMaster>> GetAllMenuAsync()
        {
            return await _context.modelMenuMasters.Where(u => u.IsActive).ToListAsync();
        }
        public async Task<ModelMenuMaster> GetMenuById(int menuid)
        {
            return _context.modelMenuMasters
                .FirstOrDefault(e => e.MenuId == menuid);
        }
        public async Task DeleteMenuAsync(ModelMenuMaster modelMenuMaster)
        {
            var existingMenu = await _context.modelMenuMasters.FindAsync(modelMenuMaster.MenuId);
            if (existingMenu != null)
            {
                existingMenu.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdatMenuAsync(ModelMenuMaster modelMenuMaster)
        {
            _context.modelMenuMasters.Update(modelMenuMaster);
            _context.SaveChanges();
        }
        public async Task<List<ModelMenuMaster>> GetMenusByUserIdAsync(long userId)
        {
            var menus = await (from um in _context.modelUserMenuMappings
                               join m in _context.modelMenuMasters on um.MenuId equals m.MenuId
                               where um.UserId == userId
                                     && um.IsActive
                                     && m.IsActive
                                     && m.IsDefault
                               select m)
                      .Distinct()
                      .ToListAsync();

            return menus;
        }
    }
}
