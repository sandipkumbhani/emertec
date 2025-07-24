using MicroService_Template.Application.Extension.Interface;
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

        [HttpGet("sentences-from-telephone")]
        public async Task<IActionResult> GetSentencesFromTelephone([FromQuery] string telephone)
        {
            if (telephone == null)
            {
                return BadRequest("Telephone number is required.");
            }

            var sentences = await _showTrancriptService.GetSentencesByTelephoneAsync(telephone);

            if (sentences == null || !sentences.Any())
            {
                return NotFound("No sentences found for this telephone number.");
            }
            var result = new ModelDimJson
            {
                TelephoneNumber = telephone,
                Sentences = sentences.Select(s => new ModelSentence { Text = s }).ToList()
            };

            return Ok(result);
        }


    }
}
