using Emertec.UI.Application.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Controllers
{
    public class MenuMappingController : Controller
    {
        IMenuMappingServices _menuMappingServices;
        IUserServices _userServices;
        IMenuMasterServices _menuMasterServices;
        public MenuMappingController(IMenuMappingServices menuMappingServices, IUserServices userServices, IMenuMasterServices menuMasterServices)
        {
            _menuMappingServices = menuMappingServices;
            _userServices = userServices;
            _menuMasterServices = menuMasterServices;
        }
        public async Task<IActionResult> MenuMappingList()
        {
            IList<ModelUserMenuMapping> MenuMappingList = await _menuMappingServices.GetAllMenuMappingAsync();
            return View("~/Views/MenuMapping/MenuMappingList.cshtml", MenuMappingList);
        }
        [HttpGet]
        public async Task<IActionResult> AddMenuMapping(int? id)
        {
            await InitViewBag();
            if (id == null)
            {
                return View(new ModelUserMenuMapping());

            }
            var user = await _menuMappingServices.GetMenuMappingByIdAsync(id.Value);
            return View("~/Views/MenuMapping/AddMenuMapping.cshtml", user);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuMapping(ModelUserMenuMapping modelUserMenuMapping)
        {
            await InitViewBag();
            string MenuIds = string.Empty;
            var selectedMenus = Request.Form["SelectedMenuIds"];
            if (selectedMenus.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one menu.");
                return View(modelUserMenuMapping);
            }
            MenuIds = string.Join(",", selectedMenus);
            modelUserMenuMapping.MenuIds = MenuIds;
            if (modelUserMenuMapping.UserMenuMappingId == 0)
            {
                await _menuMappingServices.AddMenuMappingAsync(modelUserMenuMapping);
            }
            else
            {
                await _menuMappingServices.UpdateMenuMappingAsync(modelUserMenuMapping);
            }
            return RedirectToAction("MenuMappingList");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _menuMappingServices.DeleteMenuMappingAsync(userId);
                return RedirectToAction("MenuMappingList");
            }
            catch (KeyNotFoundException ex)
            {
                ViewBag.ErrorMessage = $"User with ID {userId} not found: {ex.Message}";
                return View("Error");
            }
        }
        private async Task InitViewBag()
        {
            IList<ModelUsers> users = await _userServices.GetAllUsersAsync();
            ViewBag.UserList = users;
            IList<ModelMenuMaster> menuMasters = _menuMasterServices.GetAllMenuMasterAsync().Result;
            ViewBag.MenuMasterList = menuMasters;
        }
    }
}
