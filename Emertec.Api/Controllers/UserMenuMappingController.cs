using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
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

        [HttpPost("Menu-Master-Mapping")]
        public async Task<IActionResult> CreateMenuMasterMaster([FromBody] ModelUserMenuMapping modelUserMenuMapping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelUserMenuMappingService.CreateMenuMasterMappingAsync(modelUserMenuMapping);
            return Ok(menuMaster);
        }
        [HttpDelete("Delete-MenuMapping")]
        public async Task<IActionResult> Delete(int Menuid)
        {
            try
            {
                await _modelUserMenuMappingService.DeleteMenuMappingById(Menuid);
                return Ok($"Menu Mapping with ID {Menuid} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"Menu Mapping with ID {Menuid} not found: {ex.Message}");
            }
        }
        [HttpPut("Update-MenuMasterMapping")]
        public async Task<IActionResult> UpdateMenuAsync(int userMenuMappingId, [FromBody] ModelUserMenuMapping modelUserMenuMapping)
        {
            if (userMenuMappingId != modelUserMenuMapping.UserMenuMappingId)
            {
                return BadRequest("Menu Mapping ID mismatch.");
            }

            try
            {
                var updated = await _modelUserMenuMappingService.UpdateMenuMappingAsync(userMenuMappingId, modelUserMenuMapping);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMenuMappingById")]
        public async Task<IActionResult> GetMenuMappingById(int Menuid)
        {
            try
            {
                var result = await _modelUserMenuMappingService.GetMenuMappingDetailsById(Menuid);
                if (result == null)
                {
                    return NotFound("Menu Mapping Id Not Found");
                }
                return Ok(result); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Menu Mapping Id Not Found");
            }
        }

    }


}
