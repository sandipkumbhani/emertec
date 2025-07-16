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
        public async Task<ModelUserMenuMapping> AddMenuMasterMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            _context.modelUserMenuMappings.Add(modelUserMenuMapping);
            await _context.SaveChangesAsync();
            return modelUserMenuMapping;
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMapping()
        {
            return await _context.modelUserMenuMappings.Where(u => u.IsActive).ToListAsync();
        }
        public async Task<ModelUsers?> GetUserWithRoleAsync(long userId)
        {
            return await _context.modelUsers
                .FirstOrDefaultAsync(r => r.UserRoleId == userId);
        }

    }
}
