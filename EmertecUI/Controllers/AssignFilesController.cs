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

            IList<ModelUsers> userList = await _assignFiles.GetAllUsersAsync();
            ViewBag.UserList = userList;
            IList<string> fileNames = await _assignFiles.GetFileNamesForUserZeroAsync();
            ViewBag.FileList = fileNames;
            var model = new ModelUsers();
            return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
        }
        [HttpPost]
        //public async Task<IActionResult> AssignFilesList(AssignFilesDto model)
        //{
        //    if (model.UserId == 0 || model.SelectedFileNames == null || !model.SelectedFileNames.Any())
        //    {
        //        ModelState.AddModelError("", "Please select a user and at least one file.");
        //    }
        //    else
        //    {

        //        var jsonIds = await _assignFiles.GetJsonIdsByFileNamesAsync(model.SelectedFileNames);


        //        await _assignFiles.UpdateMenuMappingAsync(model.UserId, jsonIds);

        //        TempData["Success"] = "Files assigned successfully.";
        //        return RedirectToAction("AssignFilesList");
        //    }
        //    model.UserList = await _assignFiles.GetAllUsersAsync();
        //    model.FileList = await _assignFiles.GetFileNamesForUserZeroAsync();

        //    return View("~/Views/AssignFiles/AssignFiles.cshtml", model);
        //}

        private async Task InitViewBag()
        {
            IList<ModelUsers> users = await _assignFiles.GetAllUsersAsync();
            ViewBag.UserList = users;

        }
    }
}
