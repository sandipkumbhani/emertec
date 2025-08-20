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
    public class ForgotPasswordDbRepository : IForgotPasswordDbRepository
    {
        private readonly AppDbContext _context;
        public ForgotPasswordDbRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<ModelUsers> GetByEmailAsync(string email)
        {
            return await _context.modelUsers
                .FirstOrDefaultAsync(u => u.EmailId == email);
        }

        public async Task UpdateAsync(ModelUsers user)
        {
            _context.modelUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
