using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginUsersController : ControllerBase
    {
        private readonly IUserLoginService _userLoginService;
        public LoginUsersController(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser dto)
        {
            var token = await _userLoginService.LoginAsync(dto);
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid Login.");

            return Ok(new { token });
        }
    }
}
