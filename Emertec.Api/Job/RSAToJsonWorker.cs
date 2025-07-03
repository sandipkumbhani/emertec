using Azure.Core;
using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using Microsoft.Extensions.Options;
using Quartz;

namespace MicroService_Template.Job
{
    public class RSAToJsonWorker : IJob
    {
        private readonly IConvertRsaToJsonService _convertRsaToJson;
        private readonly DecryptRequest _decryptRequest;
        public RSAToJsonWorker(IConvertRsaToJsonService convertRsaToJson, IOptions<DecryptRequest> decryptRequest)
        {
            _convertRsaToJson = convertRsaToJson;
            _decryptRequest = decryptRequest.Value;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _convertRsaToJson.WorkerMp3ToJson(_decryptRequest, _decryptRequest.PrivateKeyPath, _decryptRequest.whisperExePath);
            Console.WriteLine($"[Job2] convert all {result.Count} RSA TO MP3 {DateTime.Now}");
           
        }
    }
}
