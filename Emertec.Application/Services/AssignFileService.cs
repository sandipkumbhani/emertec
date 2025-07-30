using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Services
{
    public class AssignFileService : IAssignFileService
    {
        private readonly IAssignFileRepository _assignFileRepository;
        public AssignFileService(IAssignFileRepository assignFileRepository)
        {
            _assignFileRepository = assignFileRepository ?? throw new ArgumentNullException(nameof(assignFileRepository));
        }

        public async Task<List<string>> GetFileNamesForUserZeroAsync()
        {
            return await _assignFileRepository.GetFileNamesByUserIdZeroAsync();
        }
        //public async Task<bool> UpdateUserIdsByJsonIdAsync(long userId, List<Guid> jsonIds)
        // {
        //     if (jsonIds == null || !jsonIds.Any())
        //     {
        //         return false; 
        //     }
        //     await _assignFileRepository.AssignfileAsync(userId,jsonIds);
        //     return true; 
        // }

        //public async Task  AssignUserToFilesAsync(long userId, List<Guid> jsonIds)
        //{
        //    var filesToUpdate = await _assignFileRepository.GetFilesByJsonIdsAsync(jsonIds);

        //    foreach (var file in filesToUpdate)
        //    {
        //        file.UserId = userId;
        //        await _assignFileRepository.UserUpdateAsync(file);
        //    }
        //}
        public async Task AssignUserToFilesAsync(int userId, List<Guid> jsonIds)
        {
            var filesToUpdate = await _assignFileRepository.GetFilesByJsonIdsAsync(jsonIds);

            foreach (var file in filesToUpdate)
            {
                if (file.UserId == 0)
                {
                    file.UserId = userId;
                }
            }

            await _assignFileRepository.SaveChangesAsync();
        }

    }
}
