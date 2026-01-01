using Microsoft.AspNetCore.Mvc;
using PFL_API.Models.DTO;

namespace PFL_API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersWithProfilesAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfile(
            [FromBody] CreateUserProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profileId = await _userService.CreateUserProfileAsync(dto);

            return Ok(new
            {
                message = "User & profile created successfully",
                profileId
            });
        }
    }
}
