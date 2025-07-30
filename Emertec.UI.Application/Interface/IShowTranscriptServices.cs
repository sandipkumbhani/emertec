using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IShowTranscriptServices
    {
        Task<List<ModelDimJson>> GetFileNameByIsTranscriptedAsync();
        Task<ModelDimJson> GetByFileNameAsync(string fileName);
    }
}
