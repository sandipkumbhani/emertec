using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Interface;
using MicroService_Template.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AudioController : ControllerBase
    {
        private readonly IAudioFileService _audioService;
        private readonly AudioPaths _paths;
        public AudioController(IAudioFileService audioService, IOptions<AudioPaths> paths)
        {
            _audioService = audioService;
            _paths = paths.Value;

        }
        [HttpPost("convert-all-mp3-to-rsa")]
        public IActionResult ConvertAllMp3ToRsa()
        {
            var rsaFiles = _audioService.ConvertAllMp3FilesToRsaAndGuid(_paths);

            return Ok(new
            {
                Message = "All MP3 files processed successfully.",
                Files = rsaFiles
            });

        }
        [HttpPost("get-all-rsa-and-guid")]
        public IActionResult GetAllRsaAndGuid()
        {
            var rsaFiles = _audioService.ConvertAllRsaFilesToJson(_paths);

            return Ok(new
            {
                Message = "All files get successfully.",
                Files = rsaFiles
            });
        }
    }
}




