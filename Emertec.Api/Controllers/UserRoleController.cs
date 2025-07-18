using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("Create-User-Role")]
        public async Task<IActionResult> CreateMenuMaster([FromBody] ModelUserRole modelUserRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelUserRoleService.CreateUserRoleAsync(modelUserRole);
            return Ok(menuMaster);
        }
        [HttpDelete("Delete-UserRole")]
        public async Task<IActionResult> Delete(int roleid)
        {
            try
            {
                 await _modelUserRoleService.DeleteUserRoleById(roleid);
                return Ok($"User with ID {roleid} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"User Role with ID {roleid} not found: {ex.Message}");
            }
        }
        [HttpPut("Update-UserRole")]
        public IActionResult UpdateUserAsync(int roleid, [FromBody] ModelUserRole modelUserRole)
        {
            if (roleid != modelUserRole.UserRoleId)
            {
                return BadRequest("User Role ID mismatch.");
            }
            try
            {
                var updated = _modelUserRoleService.UpdateUserRoleAsync(roleid, modelUserRole);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)

        {
            try
            {
                var menumaster = _modelUserRoleService.GetUserRoleById(id);
                return Ok(menumaster);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }




    }
}
