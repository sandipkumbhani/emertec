using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IShowTranscriptRepository
    {
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptedAsync();
        Task<ModelDimJson> GetByFileNameAsync(string fileName);
        Task<bool> MarkIsTranscriptAsync(string fileName, long loggedInUserId);
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync();
    }
}
