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
    public class ModelCreateUserRepository : IModelCreateUserRepository
    {
        private readonly AppDbContext _context;

        public ModelCreateUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ModelUsers> AddUserAsync(ModelUsers user)
        {
            _context.modelUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.modelUsers.AnyAsync(u => u.EmailId == email);
        }
        public async Task<List<ModelUsers>> GetAllUsersAsync()
        {
            return await _context.modelUsers.Include(x => x.UserRole).Where(u => u.IsActive).ToListAsync();
        }
        public ModelUsers GetUserById(int id)
        {
            return _context.modelUsers
                .Include(e => e.UserRole)
                .FirstOrDefault(e => e.UserId == id);
        }
        public async Task DeleteAsync(ModelUsers modelUsers)
        {
            var existingMenu = await _context.modelUsers.FindAsync(modelUsers.UserId);
            if (existingMenu != null)
            {
                existingMenu.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UserUpdateAsync(ModelUsers modelUsers)
        {
            _context.modelUsers.Update(modelUsers);
            _context.SaveChanges();
        }
       

    }
}
