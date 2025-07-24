using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Infrastructure.Repository
{
    public class ShowTrancriptRepository : IShowTrancriptRepository
    {
        private readonly AppDbContext _context;
        public ShowTrancriptRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ModelDimJson?> GetByTelephoneNumberAsync(string telephoneNumber)
        {
            return await _context.modelDimJson
                .FirstOrDefaultAsync(x => x.TelephoneNumber == telephoneNumber);
        }
    }
}

