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
        public async Task<List<Guid>> GetJsonIdsByFileNamesAsync(List<string> fileNames)
        {
            return await _assignFileRepository.GetjsonidByFileNmaeAsync(fileNames);
        }
        public async Task AssignUserToFilesAsync(int userId, List<Guid> jsonIds,long loggedInUserId)
        {
            var filesToUpdate = await _assignFileRepository.GetFilesByJsonIdsAsync(jsonIds);

            foreach (var file in filesToUpdate)
            {
                if (file.UserId == 0)
                {
                    file.UserId = userId;
                    file.InsertBy = loggedInUserId;

                }
            }

            await _assignFileRepository.SaveChangesAsync();
        }
    }
}
