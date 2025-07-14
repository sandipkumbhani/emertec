using Azure.Core;
using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AudioController : ControllerBase
    {
        private readonly IConvertRsaToJsonService _convertRsaToJson;
        private readonly IConvertJsonToDbService _convertJsonToDb;
        private readonly IAudioFileService _audioService;
        private readonly AudioPathsDTO _paths;
        private readonly DecryptRequestDTO _decryptRequest;
        private readonly ICryptoService _cryptoService;
        private readonly JsonToDbDTO _jsontodb;

        public AudioController(IConvertRsaToJsonService convertRsaToJson, IConvertJsonToDbService convertJsonToDb, IAudioFileService audioFileService, ICryptoService cryptoService, IOptions<AudioPathsDTO> paths, IOptions<DecryptRequestDTO> decryptRequest, IOptions<JsonToDbDTO> jsontodb)
        {
            _convertRsaToJson = convertRsaToJson;
            _convertJsonToDb = convertJsonToDb;
            _audioService = audioFileService;
            _paths = paths.Value;
            _decryptRequest = decryptRequest.Value;
            _cryptoService = cryptoService;
            _jsontodb = jsontodb.Value;
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
        [HttpPost("mp3-to-json")]
        public async Task<IActionResult> DecryptAll()
        {
            try
            {
                var request = new DecryptRequestDTO
                {
                    BasePath = _decryptRequest.BasePath,
                    PrivateKeyPath = _decryptRequest.PrivateKeyPath,
                    whisperExePath = _decryptRequest.whisperExePath
                };

                var result = await _convertRsaToJson.WorkerMp3ToJson(request, request.PrivateKeyPath, request.whisperExePath);
                return Ok(new { Count = result.Count, Files = result });
            }
            catch (Exception ex)
            {
                return BadRequest($"Decryption failed: {ex.Message}");
            }
        }
        [HttpPost("generate-keys")]
        public IActionResult GenerateKeys([FromQuery] string outputFolder = @"D:\Keys")
        {
            var result = _cryptoService.GenerateRsaKeys(outputFolder);
            return Ok(result);
        }
        //[HttpPost("process-json-files")]
        //public async Task<IActionResult> GetAllJson()
        //{
        //    var jsonToDb = new JsonToDB
        //    {
        //        BasePath = _jsontodb.BasePath
        //    };

        //    var updatedRows = await _convertJsonToDb.CheckGuidFromJsonAsync(jsonToDb);

        //    return Ok("All Data Save in Database");

        //}


    }
}