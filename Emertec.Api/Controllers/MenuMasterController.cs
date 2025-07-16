using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Application.Services;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
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
        public async Task<IActionResult> CreateMenuMaster([FromBody] MenuMasterDTO menuMasterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuMaster = await _modelMenuMasterService.CreateMenuMasterAsync(menuMasterDTO);
            return Ok(menuMaster);
        }
       
    }
}
