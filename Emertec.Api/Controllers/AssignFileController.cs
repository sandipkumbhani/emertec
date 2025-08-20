using Azure.Core;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MicroService_Template.Controllers
{
    [Route("api/[controller]")]
    public class AssignFileController : Controller
    {
        private readonly IAssignFileService _assignFileService;

        public AssignFileController(IAssignFileService assignFileService)
        {
            _assignFileService = assignFileService ?? throw new ArgumentNullException(nameof(assignFileService));
        }
        [HttpGet("GetFileNameByUserId")]
        public async Task<IActionResult> GetFileNameByUserIdAsync()
        {
            List<string> fileNames = await _assignFileService.GetFileNamesForUserZeroAsync();

            if (fileNames == null || fileNames.Count == 0)
            {
                return NotFound("No files found for UserId = 0.");
            }

            return Ok(fileNames);
        }
        [HttpGet("GetJsonIdsByFileNames")]
        public async Task<IActionResult> GetJsonIdsByFileNamesAsync([FromQuery] List<string> fileNames)
        {
            if (fileNames == null || fileNames.Count == 0)
            {
                return BadRequest("File names cannot be null or empty.");
            }
            var jsonIds = await _assignFileService.GetJsonIdsByFileNamesAsync(fileNames);
            return Ok(jsonIds);
        }
        [HttpPost("AssignUserToFiles")]
        public async Task<IActionResult> AssignUserToFiles([FromBody] AssignFilesDto assignFilesDto)
        {
            await _assignFileService.AssignUserToFilesAsync(assignFilesDto.UserId, assignFilesDto.JsonIds, assignFilesDto.loggedInUserId);
            return Ok(new { message = "User assigned to files successfully." });
        }
    }
}
