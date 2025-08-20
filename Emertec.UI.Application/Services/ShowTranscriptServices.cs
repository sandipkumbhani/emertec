using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Services
{
    public class ShowTranscriptServices : IShowTranscriptServices
    {
        private readonly IShowTranscriptRepository _showTranscriptRepository;
        public ShowTranscriptServices(IShowTranscriptRepository showTranscriptRepository)
        {
            _showTranscriptRepository = showTranscriptRepository;
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptedAsync()
        {
            return await _showTranscriptRepository.GetFileNameByIsTranscriptedAsync();
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync()
        {
            return await _showTranscriptRepository.GetFileNameByIsTranscriptTrueAsync();
        }
        public async Task<ModelDimJson> GetByFileNameAsync(string fileName)
        {
            return await _showTranscriptRepository.GetByFileNameAsync(fileName);
        }
        public async Task<bool> MarkIsTranscriptAsync(string filename,long loggedInUserId)
        {
            return await _showTranscriptRepository.MarkIsTranscriptAsync(filename, loggedInUserId);
        }
    }
}
