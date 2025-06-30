using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using Microsoft.Extensions.Options;
using Quartz;

namespace MicroService_Template.Job
{
    public class WorkerJsonToDb
    {

        private readonly IConvertJsonToDbService _convertJsonToDbService;
        private readonly JsonToDB _jsonToDB;
        public WorkerJsonToDb(IConvertJsonToDbService convertJsonToDbService, IOptions<JsonToDB> jsontodb)
        {
            _convertJsonToDbService = convertJsonToDbService;
            _jsonToDB = jsontodb.Value;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _convertJsonToDbService.CheckGuidFromJsonAsync(_jsonToDB);
            Console.WriteLine($"[Job] Save All Data in Database {result.Count}{DateTime.Now}");

        }

    }
}
