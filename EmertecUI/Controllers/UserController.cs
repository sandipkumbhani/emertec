
using Emertec.UI.Application.Interface;
using Emertec.UI.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static Azure.Core.HttpHeader;

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
            IList<ModelUsers> UserList = await _userServices.GetAllUsersAsync();
            ViewBag.UserList = UserList;
            return View("~/Views/User/UserList.cshtml");
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            InitViewBag();
            ViewBag.NameMsg = string.Empty;
            ModelUsers modelUsers = new ModelUsers();
            return View("~/Views/User/AddUser.cshtml", modelUsers);
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
                InitViewBag();
                return View(modelUsers);
            }

            var user = await _userServices.AddUserAsync(modelUsers);
            ViewBag.msg = "User added successfully!";
            return RedirectToAction("UserList");
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            try
            {
                var user = await _userServices.GetUserByIdAsync(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with ID {id} not found.";
                    return View();
                }

                return View(user);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult>EditUser(int id, ModelUsers user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                var result = await _userServices.UpdateUserAsync(user);
                TempData["SuccessMessage"] = "User updated successfully.";
                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Update failed: " + ex.Message);
                return View(user);
            }
        }



        private void InitViewBag()
        {
            IList<ModelUserRole> userRoles = _userServices.GetAllUserRoleAsync().Result;
            ViewBag.RoleList = userRoles;
        }
    }
}
