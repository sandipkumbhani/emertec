
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
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
        public IActionResult ShowTranscript()
        {
            return View(new ModelDimJson());
        }

        [HttpPost]
        public async Task<IActionResult> ShowTranscript(string telephoneNo)
        {
            if (string.IsNullOrWhiteSpace(telephoneNo))
            {
                ViewBag.Error = "Please enter a valid telephone number.";
                return View(new ModelDimJson());
            }

            var modelDimJson = await _showTranscriptServices.GetTranscriptsByTelephoneNoAsync(telephoneNo);
            if (modelDimJson == null)
            {
                ViewBag.Error = "Transcript not found.";
                return View(new ModelDimJson());
            }

            return View(modelDimJson);
        }

    }
}
