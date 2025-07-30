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

        public async Task<ModelDimJson?> GetByFileNameAsync(string fileName)
        {
            return await _context.modelDimJson
                .FirstOrDefaultAsync(x => x.FileName == fileName);
        }
        public async Task<List<ModelDimJson>> GetFileNamesByIsTranscriptAsync()
        {
            return await _context.modelDimJson.Where(x => x.IsTrascripted == false)
                .ToListAsync();
        }
    }
}

