using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IShowTranscriptServices
    {
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptedAsync();
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync();
        Task<ModelDimJson> GetByFileNameAsync(string fileName);
        Task<bool> MarkIsTranscriptAsync(string filename,long loggedInUserId);
    }
}
