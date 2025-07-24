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
    public class MenuMasterController : Controller
    {
        private readonly IModelMenuMasterService _modelMenuMasterService;

        public MenuMasterController(IModelMenuMasterService modelMenuMasterService)
        {
            _modelMenuMasterService = modelMenuMasterService;
        }
        [HttpGet("Get-All-Menu-Master")]
        public async Task<IActionResult> GetAllMenuMaster()
        {

            var users = await _modelMenuMasterService.GetModelMenuMastersAsync();
            return Ok(users);
        }
        [HttpPost("Menu-Master")]
        public async Task<IActionResult> CreateMenuMaster([FromBody] ModelMenuMaster modelMenuMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelMenuMasterService.CreateMenuMasterAsync(modelMenuMaster);
            return Ok(menuMaster);
        }
        [HttpDelete("Delete-Menu-Master")]
        public async Task<IActionResult> DeleteMenuAsync(int id)
        {
            try
            {
                await _modelMenuMasterService.DeleteMenuById(id);
                return Ok($"Menu with ID {id} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"Menu with ID {id} not found: {ex.Message}");
            }
        }
        [HttpPut("Update-Menu/{menuid}")]
        public async Task<IActionResult> UpdateMenuAsync(int menuid, [FromBody] ModelMenuMaster modelMenuMaster)
        {
            var existingUser = await _modelMenuMasterService.GetMenuMsaterById(menuid);
            if (existingUser == null && menuid != modelMenuMaster.MenuId)
            {
                return BadRequest("Menu ID mismatch.");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var updatedUser = await _modelMenuMasterService.UpdateMenuAsync(menuid, modelMenuMaster);
                    return Ok(updatedUser);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = ex.Message });
                }
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)

        {
            try
            {
                var menumaster = await _modelMenuMasterService.GetMenuMsaterById(id);
                return Ok(menumaster);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
