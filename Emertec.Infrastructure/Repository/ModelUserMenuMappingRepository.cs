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
            //ModelUserMenuMapping modelUserMenuMapping = new ModelUserMenuMapping();
            //IList<ModelUserMenuMapping> ModelUserMenuMappingList = await _context.modelUserMenuMappings.Where(e => e.UserId == UserId).ToListAsync();
            //if (ModelUserMenuMappingList.Count > 0)
            //{
            //    modelUserMenuMapping = ModelUserMenuMappingList.First();
            //    string MenuIds = string.Empty;
            //    foreach (var ModelUserMenuMapping in ModelUserMenuMappingList)
            //    {
            //        MenuIds += ModelUserMenuMapping.MenuId.ToString() + ",";
            //    }
            //    MenuIds = MenuIds.Substring(0, MenuIds.Length - 1);
            //    modelUserMenuMapping.MenuIds = MenuIds;
            //}
            //return modelUserMenuMapping;
            var mappings = await _context.modelUserMenuMappings
       .Where(m => m.UserId == UserId && m.IsActive)
       .ToListAsync();

            var model = new ModelUserMenuMapping
            {
                UserId = UserId,
                MenuIds = string.Join(",", mappings.Select(m => m.MenuId)),
                UserMenuMappingId = mappings.FirstOrDefault()?.UserMenuMappingId ?? 0
            };

            return model;
        }

        public async Task DeleteMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            var existingMenu = await _context.modelUserMenuMappings
                .FirstOrDefaultAsync(m => m.UserMenuMappingId == modelUserMenuMapping.UserMenuMappingId);

            if (existingMenu != null)
            {
                existingMenu.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            _context.modelUserMenuMappings.Update(modelUserMenuMapping);
            _context.SaveChanges();
        }


    }
}
