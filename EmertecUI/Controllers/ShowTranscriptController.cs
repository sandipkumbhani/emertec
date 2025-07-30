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
        public async Task<IActionResult> ShowTranscript()
        {
            IList<ModelDimJson> fileNames = await _showTranscriptServices.GetFileNameByIsTranscriptedAsync();
            ViewBag.FileList = fileNames;
            var model = new ModelDimJson();
            return View("~/Views/ShowTranscript/ShowTranscript.cshtml", model);
        }
        [HttpGet]
        public async Task<IActionResult> ShowTranscript(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return View(new ModelDimJson());
            }
            var modelDimJson = await _showTranscriptServices.GetByFileNameAsync(fileName);
            if (modelDimJson == null || modelDimJson.Sentences == null || !modelDimJson.Sentences.Any())
            {
                ViewBag.Error = "Transcript not found.";
                return View(new ModelDimJson());
            }
            return View(modelDimJson);
        }
    }
}
