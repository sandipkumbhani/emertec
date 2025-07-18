
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Controllers
{
    public class MenuMasterController : Controller
    {
        IMenuMasterServices _menuMasterServices;

        public MenuMasterController(IMenuMasterServices menuMasterServices)
        {
            _menuMasterServices = menuMasterServices;
        }

        public async Task<IActionResult> MenuMasterList()
        {
            IList<ModelMenuMaster> MenuMasterList = await _menuMasterServices.GetAllMenuMasterAsync();
            ViewBag.MenuMasterList = MenuMasterList;
            return View("~/Views/MenuMaster/MenuMasterList.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddMenuMaster(int? id)
        {
            if (id == null)
            {
                return View(new ModelMenuMaster());
            }
            var user = await _menuMasterServices.GetMenuByIdAsync(id.Value);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuMaster(ModelMenuMaster modelMenuMaster)
        {
            string NameMsg = string.Empty;
            if (string.IsNullOrEmpty(modelMenuMaster.Name))
            {
                NameMsg = "Please Enter Name.";
                ViewBag.NameMsg = NameMsg;
            }
            string DescriptionMsg = string.Empty;
            if (string.IsNullOrEmpty(modelMenuMaster.Description))
            {
                DescriptionMsg = "Please Enter Description.";
                ViewBag.DescriptionMsg = DescriptionMsg;
            }
            if (ViewBag.NameMsg != null || ViewBag.DescriptionMsg != null)
            {
                return View(modelMenuMaster);
            }
            if (modelMenuMaster.MenuId == 0)
            {
                await _menuMasterServices.AddMenuAsync(modelMenuMaster);
            }
            else
            {
                await _menuMasterServices.UpdateMenuAsync(modelMenuMaster);

            }
            return RedirectToAction("MenuMasterList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                await _menuMasterServices.DeleteMenuAsync(id);
                return RedirectToAction("MenuMasterList");
            }
            catch (KeyNotFoundException ex)
            {
                ViewBag.ErrorMessage = $"User with ID {id} not found: {ex.Message}";
                return View("Error");
            }
        }
    }
}
