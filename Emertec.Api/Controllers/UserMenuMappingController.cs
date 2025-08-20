using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMenuMappingController : Controller
    {
        private readonly IModelUserMenuMappingService _modelUserMenuMappingService;
        public UserMenuMappingController(IModelUserMenuMappingService modelUserMenuMappingService)
        {
            _modelUserMenuMappingService = modelUserMenuMappingService;
        }
        [HttpGet("get-all-Menu-Mapping")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _modelUserMenuMappingService.GetAllMenuMappingAsync();
            return Ok(users);
        }

        [HttpPost("Create-Menu-Mapping")]
        public async Task<IActionResult> CreateMenuMasterMaster([FromBody] ModelUserMenuMapping modelUserMenuMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelUserMenuMappingService.CreateMenuMasterMappingAsync(modelUserMenuMapping);
            return Ok(menuMaster);
        }
        [HttpDelete("Delete-Menu-Mapping")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                await _modelUserMenuMappingService.DeleteMenuMappingById(userId);
                return Ok($"User with ID {userId} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"User with ID {userId} not found: {ex.Message}");
            }
        }


        [HttpPut("Update-MenuMasterMapping/{userId}")]
        public async Task<IActionResult> UpdateMenuAsync(int userId, [FromBody] ModelUserMenuMapping modelUserMenuMapping)
        {
            if (userId != modelUserMenuMapping.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                var updated = await _modelUserMenuMappingService.UpdateMenuMappingAsync(userId, modelUserMenuMapping);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMenuMappingById")]
        public async Task<IActionResult> GetMenuMappingById(int userId)
        {
            try
            {
                var result = await _modelUserMenuMappingService.GetMenuMappingDetailsById(userId);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu Mapping not found.");
            }
        }

    }


}
