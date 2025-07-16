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
    public class ModelUserLoginRepository : IModelUserLoginRepository
    {
        private readonly AppDbContext _context;

        public ModelUserLoginRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelUsers?> GetByEmailAsync(string email)
        {
            return await _context.modelUsers.FirstOrDefaultAsync(u => u.EmailId == email && u.IsActive);
        }
        public async Task<ModelUserRole?> GetUserWithRoleAsync(int userRoleId)
        {
            return await _context.modelUserRoles
                .FirstOrDefaultAsync(r => r.UserRoleId == userRoleId);
        }


    }

}
