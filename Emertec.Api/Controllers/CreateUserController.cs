using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly IModelCreateUserService _modelCreateUserService;

        public CreateUserController(IModelCreateUserService modelCreateUserService)
        {
            _modelCreateUserService = modelCreateUserService;

        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _modelCreateUserService.CreateUserAsync(createUserDTO);
            return Ok(user);
        }

    }
}
