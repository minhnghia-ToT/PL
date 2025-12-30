using Microsoft.AspNetCore.Mvc;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;

namespace PFL_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoadingPFController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoadingPFController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("users-with-profiles")]
        public async Task<IActionResult> GetUsersWithProfiles()
        {
            var users = await _userService.GetAllUsersWithProfilesAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserProfileDto dto)
        {
            var userId = await _userService.CreateProfileAsync(dto);
            return Ok(new
            {
                Message = "User created successfully",
                UserId = userId
            });
        }
    }
}
