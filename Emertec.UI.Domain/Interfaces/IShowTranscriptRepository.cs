using MicroService_Template.Domain.Model;

namespace Emertec.UI.Domain.Interfaces
{
    public interface IShowTranscriptRepository
    {
        Task<ModelDimJson> GetTranscriptsByTelephoneNoAsync(string Telephoneno);
    }
}
