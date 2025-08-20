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
            if (fileNames == null)
            {
                throw new Exception("No transcripted files found.");
            }
            ViewBag.FileList = fileNames;
            IList<ModelDimJson> IsTranscript = await _showTranscriptServices.GetFileNameByIsTranscriptTrueAsync();
            if (IsTranscript == null || !IsTranscript.Any())
            {
                ViewBag.ErrorMessage = "No transcripted files found.";
            }

            ViewBag.IsTranscript = IsTranscript;
            var model = new ModelDimJson();
            return View("~/Views/ShowTranscript/ShowTranscript.cshtml", model);
        }
        [HttpGet("ShowTranscript/View")]
        public async Task<IActionResult> ShowTranscriptByFile(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                ViewBag.Error = "File name is required.";
                return PartialView("_TranscriptContent", new ModelDimJson());
            }
            var modelDimJson = await _showTranscriptServices.GetByFileNameAsync(fileName);

            if (modelDimJson == null || modelDimJson.Sentences == null || !modelDimJson.Sentences.Any())
            {
                ViewBag.Error = "Transcript not found.";
                return PartialView("_TranscriptContent", new ModelDimJson());
            }
            return PartialView("_TranscriptContent", modelDimJson);
        }
        [HttpGet("ShowTranscript/NewPage")]
        public async Task<IActionResult> ShowTranscriptInNewPage(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                ViewBag.Error = "File name is required.";
                return View("_TranscriptContentNewPage", new ModelDimJson());
            }
            var modelDimJson = await _showTranscriptServices.GetByFileNameAsync(fileName);
            if (modelDimJson == null || modelDimJson.Sentences == null || !modelDimJson.Sentences.Any())
            {
                ViewBag.Error = "Transcript not found.";
                return View("_TranscriptContentNewPage", new ModelDimJson());
            }
            ViewBag.FileName = fileName;
            return View("_TranscriptContentNewPage", modelDimJson);
        }
        [HttpPost]
        public async Task<IActionResult> MarkAsTranscripted(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                return BadRequest("File name is required.");
            }
            var userIdClaim = HttpContext.User?.FindFirst("UserId")?.Value;
            long.TryParse(userIdClaim, out long loggedInUserId);
            bool success = await _showTranscriptServices.MarkIsTranscriptAsync(filename, loggedInUserId);
            if (success)
            {
                return RedirectToAction("ShowTranscript", "ShowTranscript");
            }
            else
            {
                TempData["Error"] = "File not found or already marked.";
                return RedirectToAction("FileList", "ShowTranscript");
            }
        }
    }
}
