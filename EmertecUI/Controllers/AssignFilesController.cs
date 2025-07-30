using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using Emertec.UI.Domain.AssignFiles;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

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
            if (ViewBag.UserError != null || ViewBag.FileError != null)
            {
                // Re-populate dropdowns if validation fails
                model.UserList = await _assignFiles.GetAllUsersAsync();
                model.FileList = await _assignFiles.GetFileNamesForUserZeroAsync();
                return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
            }
            model.UserList = await _assignFiles.GetAllUsersAsync();
            model.FileList = await _assignFiles.GetFileNamesForUserZeroAsync();
            TempData["Success"] = "Files assigned successfully.";

        //    return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
        //}


        private async Task InitViewBag()
        {
            IList<ModelUsers> users = await _assignFiles.GetAllUsersAsync();
            ViewBag.UserList = users;

        }
    }
}
