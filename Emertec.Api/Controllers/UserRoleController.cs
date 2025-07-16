using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IModelUserRoleService _modelUserRoleService;

        public UserRoleController(IModelUserRoleService modelUserRoleService)
        {
            _modelUserRoleService = modelUserRoleService;
        }
        [HttpGet("get-all-userRole")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersRole = await _modelUserRoleService.GetAllUsersRoleAsync();
            return Ok(usersRole);
        }
        [HttpPost("User-Role")]
        public async Task<IActionResult> CreateMenuMaster([FromBody] ModelUserRole modelUserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelUserRoleService.CreateUserRoleAsync(modelUserRole);
            return Ok(menuMaster);
        }
        



    }
}
