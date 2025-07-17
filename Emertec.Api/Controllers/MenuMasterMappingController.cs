using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuMasterMappingController : Controller
    {

        private readonly IModelUserMenuMappingService _modelUserMenuMappingService;
        public MenuMasterMappingController(IModelUserMenuMappingService modelUserMenuMappingService)
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
        [HttpDelete("Delete-MenuMasterMapping")]
        public async Task<IActionResult> DeleteMenuAsync(int userMenuMappingId)
        {
            try
            {
                await _modelUserMenuMappingService.DeleteMenuMappingById(userMenuMappingId);
                return Ok($"Menu with ID {userMenuMappingId} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"Menu Mapping with ID {userMenuMappingId} not found: {ex.Message}");
            }
        }
        [HttpPut("Update-MenuMasterMapping")]
        public IActionResult UpdateMenuAsync(int userMenuMappingId, [FromBody] ModelUserMenuMapping modelUserMenuMapping)
        {
            if (userMenuMappingId != modelUserMenuMapping.UserMenuMappingId)
            {
                return BadRequest("Menu Mapping ID mismatch.");
            }
            try
            {
                var updated = _modelUserMenuMappingService.UpdateMenuMappingAsync(userMenuMappingId, modelUserMenuMapping);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


    }
}
