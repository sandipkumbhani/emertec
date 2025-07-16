using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IModelCreateUserService _modelCreateUserService;

        public UserController(IModelCreateUserService modelCreateUserService)
        {
            _modelCreateUserService = modelCreateUserService;

        }
        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _modelCreateUserService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ModelUsers modelUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _modelCreateUserService.CreateUserAsync(modelUsers);
            return Ok(user);
        }

    }
}
