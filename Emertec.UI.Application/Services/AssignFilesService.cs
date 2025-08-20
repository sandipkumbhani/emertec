using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Services
{
    public class AssignFilesService : IAssignFilesService
    {
        private readonly IAssignFilesRepository _assignFilesRepository;
        public AssignFilesService(IAssignFilesRepository assignFilesRepository)
        {
            _assignFilesRepository = assignFilesRepository;
        }
        public async Task<List<ModelUsers>> GetAllUsersAsync()
        {
            return await _assignFilesRepository.GetAllUsersAsync();
        }
        public async Task<List<string>> GetFileNamesForUserZeroAsync()
        {
            return await _assignFilesRepository.GetFileNamesForUserZeroAsync();
        }
        public async Task<List<Guid>> GetJsonIdsByFileNamesAsync(List<string> fileNames)
        {
            var stringIds = await _assignFilesRepository.GetjsonidByFileNmaeAsync(fileNames);
            return stringIds.Select(Guid.Parse).ToList();
        }
        public async Task<bool> UpdateUserIdAsync(int userId,List<Guid> jsonIds,long loggedInUserId)
        {
            if (jsonIds == null || loggedInUserId == null)
            {
                return false; 
            }
            return await _assignFilesRepository.AssignUserToFilesAsync(userId, jsonIds, loggedInUserId);
        }
    }
}
