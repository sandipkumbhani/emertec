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
        public async Task<ModelDimJson> GetTranscriptsByTelephoneNoAsync(string Telephoneno)
        {
            return await _showTranscriptRepository.GetTranscriptsByTelephoneNoAsync(Telephoneno);
        }
    }
}
