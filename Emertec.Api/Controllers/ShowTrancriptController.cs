using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static MicroService_Template.Domain.Model.ModelDimJson;

namespace MicroService_Template.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTrancriptController : Controller
    {
        private readonly IShowTrancriptService _showTrancriptService;
        public ShowTrancriptController(IShowTrancriptService showTrancriptService)
        {
            _showTrancriptService = showTrancriptService ?? throw new ArgumentNullException(nameof(showTrancriptService));
        }
        [HttpGet("GetFileNameByIsTranscripted")]
        public async Task<IActionResult> GetFileNameByIsTranscriptedAsync()
        {
            List<ModelDimJson> fileNames = await _showTrancriptService.GetFileNameByIsTranscript();

            if (fileNames == null || fileNames.Count == 0)
            {
                throw new Exception("FileName is null");
            }
            return Ok(fileNames);
        }
        [HttpGet("sentences-from-FileName")]
        public async Task<IActionResult> GetByFileNameAsync([FromQuery] string fileName)
        {
            if (fileName == null)
            {
                return BadRequest("File Name is required.");
            }

            var sentences = await _showTrancriptService.GetByFileNameAsync(fileName);

            if (sentences == null || !sentences.Any())
            {
                return NotFound("No sentences found for this File Name.");
            }
            var result = new ModelDimJson
            {
                FileName = fileName,
                Sentences = sentences.Select(s => new ModelSentence { Text = s }).ToList()
            };

            return Ok(result);
        }


    }
}
