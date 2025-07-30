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
        public async Task<ModelDimJson> GetByFileNameAsync(string fileName)
        {
            return await _showTranscriptRepository.GetByFileNameAsync(fileName);
        }
    }
}
