using MicroService_Template.Domain.Model;

namespace Emertec.UI.Application.Interface
{
    public interface IShowTranscriptServices
    {
        Task<ModelDimJson> GetTranscriptsByTelephoneNoAsync(string Telephoneno);
    }
}
