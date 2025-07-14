using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ModelUserLogin> GetByEmailAsync(string email)
        {
            return await _context.modelUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(ModelUserLogin user)
        {
            _context.modelUsers.Add(user);
            await _context.SaveChangesAsync();
        }
    }

}
