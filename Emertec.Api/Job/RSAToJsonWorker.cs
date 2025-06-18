using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Interface;
using Microsoft.Extensions.Options;
using Quartz;

namespace MicroService_Template.Job
{
    public class RSAToJsonWorker
    {
        private readonly IAudioFileService _audioService;
        private readonly AudioPaths _paths;

        public RSAToJsonWorker(IAudioFileService audioService, IOptions<AudioPaths> paths)
        {
            _audioService = audioService;
            _paths = paths.Value;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var result = _audioService.ConvertAllRsaFilesToJson(_paths);
            Console.WriteLine($"[Job] get all {result.Count} rsa and guid file at {DateTime.Now}");
            return Task.CompletedTask;
        }
    }
}
