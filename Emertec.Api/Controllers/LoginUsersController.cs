using Emertec.UI.Domain.Comman;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
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
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userLoginService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO userLoginRequestDto)
        {
            var result = await _userLoginService.LoginAsync(userLoginRequestDto.EmailId, userLoginRequestDto.Password);

            if (result == null)
            {
                return Unauthorized(new CommanResponseDto
                {
                    StatusCode = 401,
                    Message = "Unauthorized",
                    ErrorMessage = "Invalid Email or password"
                });
            }

            return Ok(new CommanResponseDto
            {
                StatusCode = 200,
                Message = "Login successful",
                Data = result
            });
        }

    }
}
