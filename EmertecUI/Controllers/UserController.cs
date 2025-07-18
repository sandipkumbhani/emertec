using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
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
            //var users = await _userServices.GetAllUsersAsync();
            //ViewBag.UserList = users;
            //return View(users);
            IList<ModelUsers> UserList = await _userServices.GetAllUsersAsync();
            ViewBag.UserList = UserList;
            return View("~/Views/User/UserList.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddUser(int? id)
        {
            await InitViewBag();
            if (id == null)
            {
                return View(new ModelUsers());

            }
            var user = await _userServices.GetUserByIdAsync(id.Value);
            return View(user);
           
            //ViewBag.NameMsg = string.Empty;
            //ModelUsers modelUsers = new ModelUsers();
            //return View("~/Views/User/AddUser.cshtml", modelUsers);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(ModelUsers modelUsers)
        {
            string NameMsg = string.Empty;
            if (string.IsNullOrEmpty(modelUsers.Name))
            {
                NameMsg = "Please Enter Name.";
                ViewBag.NameMsg = NameMsg;
            }
            string EmailMsg = string.Empty;
            if (string.IsNullOrEmpty(modelUsers.EmailId))
            {
                EmailMsg = "Please Enter EmailId.";
                ViewBag.EmailMsg = EmailMsg;
            }
            string passwordMsg = string.Empty;
            if (string.IsNullOrEmpty(modelUsers.Password))
            {
                passwordMsg = "Please Enter Password.";
                ViewBag.passwordMsg = passwordMsg;
            }
            string ConfirmPasswordMsg = string.Empty;
            if (modelUsers.Password != modelUsers.ConfirmPassword)
            {
                ConfirmPasswordMsg = "Password and Confirm Password do not match.";
                ViewBag.ConfirmPasswordMsg = ConfirmPasswordMsg;
            }
            string UserRoleMsg = string.Empty;
            if (modelUsers.UserRoleId == 0)
            {
                UserRoleMsg = "Please select a role.";
                ViewBag.UserRoleMsg = UserRoleMsg;
            }
            if (ViewBag.NameMsg != null || ViewBag.EmailMsg != null || ViewBag.passwordMsg != null || ViewBag.UserRoleMsg != null)
            {
                await InitViewBag();
                return View(modelUsers);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.RoleList = await _userServices.GetAllUserRoleAsync();
                return View(modelUsers);
            }

            if (modelUsers.UserId == 0)
            {
                await _userServices.AddUserAsync(modelUsers);
            }
            else
            {
                await _userServices.UpdateUserAsync(modelUsers);

            }
            return RedirectToAction("UserList");
            //var user = await _userServices.AddUserAsync(modelUsers);
            //ViewBag.msg = "User added successfully!";
            //return RedirectToAction("UserList");
        }
        private async Task InitViewBag()
        {
            IList<ModelUserRole> userRoles = await _userServices.GetAllUserRoleAsync();
            ViewBag.RoleList = userRoles;
        }
    }
}
