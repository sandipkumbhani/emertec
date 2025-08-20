using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using Emertec.UI.Domain.AssignFiles;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace EmertecUI.Controllers
{
    public class AssignFilesController : Controller
    {
        private readonly IAssignFilesService _assignFiles;
        public AssignFilesController(IAssignFilesService assignFiles)
        {
            _assignFiles = assignFiles;
        }
        [HttpGet]
        public async Task<IActionResult> AssignFilesList()
        {
            var model = new AssignFilesDto
            {
                UserList = await _assignFiles.GetAllUsersAsync(),
                FileList = await _assignFiles.GetFileNamesForUserZeroAsync()
            };
            return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignFilesList(AssignFilesDto model)
        {
            int selectedUserId = model.UserId;
            if (selectedUserId == 0)
            {
                ViewBag.UserError = "Please select a user.";
            }
            if (model.SelectedFileNames == null || !model.SelectedFileNames.Any())
            {
                ViewBag.FileError = "Please select at least one file.";
            }
            if (selectedUserId != 0 && model.SelectedFileNames != null && model.SelectedFileNames.Any())
            {
                var userIdClaim = HttpContext.User?.FindFirst("UserId")?.Value;
                long.TryParse(userIdClaim, out long loggedInUserId);
                model.JsonIds = await _assignFiles.GetJsonIdsByFileNamesAsync(model.SelectedFileNames);
                await _assignFiles.UpdateUserIdAsync(model.UserId, model.JsonIds,loggedInUserId);

                TempData["Success"] = "Files assigned successfully.";
                return RedirectToAction("AssignFilesList");
            }
            model.UserList = await _assignFiles.GetAllUsersAsync();
            model.FileList = await _assignFiles.GetFileNamesForUserZeroAsync();
            return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
        }
        private async Task InitViewBag()
        {
            IList<ModelUsers> users = await _assignFiles.GetAllUsersAsync();
            ViewBag.UserList = users;
        }
    }
}
