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
    public class ModelUserRoleRepository : IModelUserRoleRepository
    {
        private readonly AppDbContext _context;

        public ModelUserRoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelUserRole> AddUserRoleAsync(ModelUserRole modelUserRole)
        {
            _context.modelUserRoles.Add(modelUserRole);
            await _context.SaveChangesAsync();
            return modelUserRole;
        }
        public async Task<List<ModelUserRole>> GetAllUsersRole()
        {
            return await _context.modelUserRoles.Where(u => u.IsActive).ToListAsync();
        }
        public async Task<ModelUserRole> GetUserRoleById(int roleid)
        {
            return  _context.modelUserRoles
                .FirstOrDefault(e => e.UserRoleId == roleid);
        }
        public async Task DeleteRoleAsync(ModelUserRole modelUserRole)
        {
            var existingMenu = await _context.modelUserRoles.FindAsync(modelUserRole.UserRoleId);
            if (existingMenu != null)
            {
                existingMenu.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UserRoleUpdateAsync(ModelUserRole modelUserRole)
        {
            _context.modelUserRoles.Update(modelUserRole);
            _context.SaveChanges();
        }
    }
}
