using Microsoft.AspNetCore.Mvc;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;

namespace PFL_API.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Lấy profile theo userId (dùng cho trang chủ)
        /// </summary>
        /// <param name="userId">Id của User</param>
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetProfileByUserId(int userId)
        {
            var profile = await _profileService.GetProfileDetailAsync(userId);

            if (profile == null)
                return NotFound(new { message = "Profile not found" });

            return Ok(profile);
        }

        /// <summary>
        /// Tạo profile cho user (1 user chỉ có 1 profile)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _profileService.CreateProfileAsync(request);
                return Ok(new { message = "Profile created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //add
        [HttpPost("single_add")]
        public async Task<IActionResult> Add(CreateProfileRequest request)
        {
            await _profileService.AddSingleProfileAsync(request);
            return Ok();
        }
        //update
        [HttpPut("{userId:int}")]
        public async Task<IActionResult> UpdateProfile(
     int userId,
     [FromBody] CreateProfileRequest request
 )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.UserId != userId)
                return BadRequest(new { message = "UserId mismatch" });

            try
            {
                await _profileService.CreateProfileAsync(request);
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
