
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
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
            //ViewBag.MenuMappingList = MenuMappingList;
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
            ModelUserMenuMapping modelUserMenuMapping = new ModelUserMenuMapping();
            return View("~/Views/MenuMapping/AddMenuMapping.cshtml", modelUserMenuMapping);
            //ViewBag.NameMsg = string.Empty;
            //ModelUserMenuMapping modelUserMenuMapping = new ModelUserMenuMapping();
            //return View("~/Views/MenuMapping/AddMenuMapping.cshtml", modelUserMenuMapping);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuMapping(ModelUserMenuMapping modelUserMenuMapping)
        {
            //string UserRoleMsg = string.Empty;
            //if (modelUsers.UserRoleId == 0)
            //{
            //    UserRoleMsg = "Please select a role.";
            //    ViewBag.UserRoleMsg = UserRoleMsg;
            //}
            //if (ViewBag.NameMsg != null || ViewBag.EmailMsg != null || ViewBag.passwordMsg != null || ViewBag.UserRoleMsg != null)
            //{
            await InitViewBag();
            //return View(modelUsers);
            //}
            string MenuIds = string.Empty;
            if (!string.IsNullOrEmpty(Request.Form["SelectedMenuIds"]))
            {
                string[] MenuIdList = Request.Form["SelectedMenuIds"];
                foreach (var item in MenuIdList)
                {
                    MenuIds += item + ",";
                }
                MenuIds = MenuIds.Substring(0, MenuIds.Length - 1);
            }
            else
            {
                return View(modelUserMenuMapping);
            }
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
            //var user = await _menuMappingServices.AddMenuMappingAsync(modelUserMenuMapping);
            //ViewBag.msg = "User added successfully!";

        }
        [HttpGet]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _menuMappingServices.DeleteMenuMappingAsync(id);
                return RedirectToAction("UserList");
            }
            catch (KeyNotFoundException ex)
            {
                ViewBag.ErrorMessage = $"User with ID {id} not found: {ex.Message}";
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
