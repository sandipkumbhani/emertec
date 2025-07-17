
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
            ViewBag.MenuMappingList = MenuMappingList;
            return View("~/Views/MenuMapping/MenuMappingList.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddMenuMapping()
        {
            InitViewBag();
            ViewBag.NameMsg = string.Empty;
            ModelUserMenuMapping modelUserMenuMapping = new ModelUserMenuMapping();
            return View("~/Views/MenuMapping/AddMenuMapping.cshtml", modelUserMenuMapping);
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
            InitViewBag();
            //return View(modelUsers);
            //}

            var user = await _menuMappingServices.AddMenuMappingAsync(modelUserMenuMapping);
            ViewBag.msg = "User added successfully!";
            return RedirectToAction("MenuMappingList");
        }
        private void InitViewBag()
        {
            IList<ModelUsers> users = _userServices.GetAllUsersAsync().Result;
            ViewBag.UserList = users;
            IList<ModelMenuMaster> menuMasters = _menuMasterServices.GetAllMenuMasterAsync().Result;
            ViewBag.MenuMasterList = menuMasters;
        }
    }
}
