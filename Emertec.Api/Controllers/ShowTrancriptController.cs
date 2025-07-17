using MicroService_Template.Application.Extension.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
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

            return Ok(sentences);
        }


    }
}
