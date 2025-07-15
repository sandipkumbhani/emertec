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
    }
}
