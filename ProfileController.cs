using Microsoft.AspNetCore.Mvc;
using PFL_API.Models.DTO;
// Make sure the namespace matches your folder structure and project organization.
// If your interface is actually in PFL_API.Interfaces, update the using accordingly.
using PFL_API.Services.Interfaces;

[ApiController]
[Route("api/users/{userId:int}/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    // GET: api/users/1/profile
    [HttpGet]
    public async Task<IActionResult> GetProfile(int userId)
    {
        var result = await _profileService.GetProfileDetailAsync(userId);

        if (result == null)
            return NotFound("Profile not found");

        return Ok(result);
    }

    // POST: api/users/1/profile
    [HttpPost]
    public async Task<IActionResult> CreateProfile(
        int userId,
        [FromBody] CreateProfileRequest request)
    {
        await _profileService.CreateProfileAsync(userId, request);

        return Ok(new
        {
            message = "Profile created successfully"
        });
    }
}