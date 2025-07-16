
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Controllers
{
    public class UserController : Controller
    {
        IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> UserList()
        {
            IList<UserDTO> UserList = await _userServices.GetAllUsersAsync();
            ViewBag.UserList = UserList;
            return View("~/Views/User/UserList.cshtml");
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View("~/Views/User/AddUser.cshtml");
        }
        [HttpPost]
        public async Task<IActionResult> AddUserAsync(ModelUsers modelUsers)
        {
            if (ModelState.IsValid)
            {
                var user = await _userServices.AddUserAsync(modelUsers);
            }
            ViewBag.msg = "User added successfully!";
            return RedirectToAction("UserList");

        }
    }
}
