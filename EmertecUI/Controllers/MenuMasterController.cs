
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
        public async Task<IActionResult> AddMenuMaster()
        {
            InitViewBag();
            ViewBag.NameMsg = string.Empty;
            ModelMenuMaster modelMenuMaster = new ModelMenuMaster();
            return View("~/Views/MenuMaster/AddMenuMaster.cshtml", modelMenuMaster);
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
            if (ViewBag.NameMsg != null || ViewBag.EmailMsg != null)
            {
                InitViewBag();
                return View(modelMenuMaster);
            }

            var user = await _menuMasterServices.AddMenuAsync(modelMenuMaster);
            ViewBag.msg = "Menu added successfully!";
            return RedirectToAction("MenuMasterList");
        }
        private void InitViewBag()
        {
            IList<ModelMenuMaster> userRoles = _menuMasterServices.GetAllMenuMasterAsync().Result;
        }
    }
}
