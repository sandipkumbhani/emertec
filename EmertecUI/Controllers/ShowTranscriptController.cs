using Emertec.UI.Application.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Controllers
{
    public class ShowTranscriptController : Controller
    {
        IShowTranscriptServices _showTranscriptServices;
        public ShowTranscriptController(IShowTranscriptServices showTranscriptServices)
        {
            _showTranscriptServices = showTranscriptServices;
        }
        [HttpGet]
        public async Task<IActionResult> ShowTranscript(string? telephoneNo)
        {
            if (string.IsNullOrWhiteSpace(telephoneNo))
            {
                return View(new ModelDimJson());
            }
            var modelDimJson = await _showTranscriptServices.GetTranscriptsByTelephoneNoAsync(telephoneNo);
            if (modelDimJson == null || modelDimJson.Sentences == null || !modelDimJson.Sentences.Any())
            {
                ViewBag.Error = "Transcript not found.";
                return View(new ModelDimJson());
            }
            return View(modelDimJson);
        }
    }
}
