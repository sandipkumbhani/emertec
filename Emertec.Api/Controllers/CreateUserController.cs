using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        private readonly IUserRegistrationService _userService;
      
        public CreateUserController(IUserRegistrationService userService, IUserLoginService loginService)
        {
            _userService = userService;
          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registrationdto)
        {
            var success = await _userService.RegisterAsync(registrationdto);
            if (!success)
                return BadRequest("User already exists.");

            return Ok("User registered successfully.");
        }
       
    }
}
