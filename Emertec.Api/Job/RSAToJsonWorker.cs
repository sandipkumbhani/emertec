//using MicroService_Template.Application.Extension.Interface;
//using MicroService_Template.Domain.DTO;
//using MicroService_Template.Domain.Extension.Interface;
//using Microsoft.Extensions.Options;
//using Quartz;

//namespace MicroService_Template.Job
//{
//    public class RSAToJsonWorker : IJob
//    {
//        private readonly IConvertRsaToJsonService _convertRsaToJson;
//        private readonly DecryptRequestDTO _decryptRequest;
//        public RSAToJsonWorker(IConvertRsaToJsonService convertRsaToJson, IOptions<DecryptRequestDTO> decryptRequest)
//        {
//            _convertRsaToJson = convertRsaToJson;
//            _decryptRequest = decryptRequest.Value;
//        }
//        public async Task Execute(IJobExecutionContext context)
//        {
//            try
//            {
//                var result = await _convertRsaToJson.WorkerMp3ToJson(_decryptRequest, _decryptRequest.PrivateKeyPath, _decryptRequest.whisperExePath);
//                Console.WriteLine($"[Job2] convert all {result.Count} RSA TO MP3 {DateTime.Now}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[Job2] Error: {ex.Message}");

//            }
//        }
//    }
//}
