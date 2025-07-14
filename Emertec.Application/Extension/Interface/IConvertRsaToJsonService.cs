using MicroService_Template.Application.DTO;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IConvertRsaToJsonService
    {

        Task<List<string>> WorkerMp3ToJson(DecryptRequestDTO request, string privateKeyPath, string whisperExePath);

    }
}
