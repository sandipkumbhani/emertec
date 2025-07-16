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
    }
}
