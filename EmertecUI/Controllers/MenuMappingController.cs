using Emertec.UI.Application.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var role = HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = HttpContext.User?.FindFirst("UserId")?.Value;
            long.TryParse(userIdClaim, out long userId);
            IList<ModelUserMenuMapping> menuMappingList = await _menuMappingServices.GetAllMenuMappingAsync();
            if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                menuMappingList = menuMappingList
                    .Where(m => m.UserId != userId)
                    .ToList();
            }
            else
            {
                menuMappingList = menuMappingList
                    .Where(m => m.UserId == userId)
                    .ToList();
            }
            return View("~/Views/MenuMapping/MenuMappingList.cshtml", menuMappingList);
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
            bool isValid = true;
            if (modelUserMenuMapping.UserId == 0)
            {
                ViewBag.UserMsg = "Please select a user.";
                isValid = false;
            }

            var selectedMenus = Request.Form["SelectedMenuIds"];
            if (!selectedMenus.Any())
            {
                ViewBag.MenuMsg = "Please select at least one menu.";
                isValid = false;
            }
            else
            {
                modelUserMenuMapping.MenuIds = string.Join(",", selectedMenus);
            }

            if (!isValid)
            {
                return View(modelUserMenuMapping);
            }
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
