//using MicroService_Template.Application.DTO;
//using MicroService_Template.Application.Extension.Interface;
//using Microsoft.Extensions.Options;
//using Quartz;

//namespace MicroService_Template.Job
//{
//    [DisallowConcurrentExecution]
//    public class MP3ToRSAWorker : IJob
//    {
//        private readonly IAudioFileService _audioService;
//        private readonly AudioPaths _paths;

//        public MP3ToRSAWorker(IAudioFileService audioService, IOptions<AudioPaths> paths)
//        {
//            _audioService = audioService;
//            _paths = paths.Value;
//        }
//        public Task Execute(IJobExecutionContext context)
//        {
//            try
//            {
//                var result = _audioService.ConvertAllMp3FilesToRsaAndGuid(_paths);
//                Console.WriteLine($"[Job] Converted {result.Count} MP3 files to RSA at {DateTime.Now}");
//                return Task.CompletedTask;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"[Job] Error: {ex.Message}");
//                return Task.FromException(ex);
//            }
//        }
//    }
//}
