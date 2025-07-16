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
        [HttpDelete("Delete-User")]
        public async Task <IActionResult> Delete(int id)
        {
            try
            {
                _modelCreateUserService.DeleteUserById(id);
                return Ok($"User with ID {id} has been deleted successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return Ok($"User with ID {id} not found: {ex.Message}");
            }
        }
        [HttpPut("Update-User")]
        public IActionResult UpdateUserAsync(int userid, [FromBody]ModelUsers modelUsers)
        {
            if (userid != modelUsers.UserId)
            {
                return BadRequest("User ID mismatch.");
            }
            try
            {
                var updated = _modelCreateUserService.UpdateUserAsync(userid, modelUsers);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
