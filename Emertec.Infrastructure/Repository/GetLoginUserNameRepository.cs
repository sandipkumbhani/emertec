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
    public class GetLoginUserNameRepository : IGetLoginUserNameRepository
    {
        private readonly AppDbContext _context;
        public GetLoginUserNameRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<ModelUsers> GetUserNameAsync(long userid)
        {
            var userName = await _context.modelUsers
            .Where(u => u.UserId == userid)
            .Select(u => u.Name)
            .FirstOrDefaultAsync();
            return new ModelUsers
            {
                UserId = userid,
                Name = userName
            };
        }
    }
}
