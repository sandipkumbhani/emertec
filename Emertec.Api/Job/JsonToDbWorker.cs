//using MicroService_Template.Application.DTO;
//using MicroService_Template.Application.Extension.Interface;
//using Microsoft.Extensions.Options;
//using Quartz;

//namespace MicroService_Template.Job
//{
//    public class JsonToDbWorker : IJob
//    {

//        private readonly IConvertJsonToDbService _convertJsonToDbService;
//        private readonly JsonToDB _jsonToDB;
//        public JsonToDbWorker(IConvertJsonToDbService convertJsonToDbService, IOptions<JsonToDB> jsontodb)
//        {
//            _convertJsonToDbService = convertJsonToDbService;
//            _jsonToDB = jsontodb.Value;
//        }
//        public async Task Execute(IJobExecutionContext context)
//        {
//            try
//            {
//                var result = await _convertJsonToDbService.CheckGuidFromJsonAsync(_jsonToDB);
//                Console.WriteLine($"[Job3] Save All Data in Database {result.Count} at {DateTime.Now}");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[Job3] Error: {ex.Message}");
//            }
//        }

//    }
//}
