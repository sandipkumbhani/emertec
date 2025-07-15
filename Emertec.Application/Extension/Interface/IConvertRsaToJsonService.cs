using MicroService_Template.Domain.DTO;

namespace MicroService_Template.Domain.Extension.Interface
{
    public interface IConvertRsaToJsonService
    {

        Task<List<string>> WorkerMp3ToJson(DecryptRequestDTO request, string privateKeyPath, string whisperExePath);

    }
}
