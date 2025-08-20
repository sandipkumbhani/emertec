using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Http;
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
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptAsync()
        {
            var files = await _context.modelDimJson.Where(x => x.IsTrascripted == false)
                .ToListAsync();
            var userMap = await _context.modelUsers
                .ToDictionaryAsync(u => u.UserId, u => u.Name);

            foreach (var file in files)
            {
                if (userMap.TryGetValue(file.InsertBy, out var updaterName))
                {
                    file.InsertName = updaterName;
                }
            }
            return files;
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync()
        {
            var files = await _context.modelDimJson
                .Where(x => x.IsTrascripted == true)
                .ToListAsync();
            var userMap = await _context.modelUsers
               .ToDictionaryAsync(u => u.UserId, u => u.Name);

            foreach (var file in files)
            {
                if (userMap.TryGetValue(file.UpdateBy, out var updaterName))
                {
                    file.UpdeterName = updaterName;
                }
            }
            return files;
        }

        public async Task<bool> MarFileAsTranscriptedAsync(string fileName,long loggedInUserId)
        {
            var entity = await _context.modelDimJson
                .FirstOrDefaultAsync(x => x.FileName == fileName && !x.IsTrascripted);

            if (entity == null)
            {
                return false;
            }
            
            entity.IsTrascripted = true;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateBy = loggedInUserId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

