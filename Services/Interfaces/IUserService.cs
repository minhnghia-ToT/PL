using PFL_API.Models.DTO;

namespace PFL_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserProfileDto>> GetAllUsersWithProfilesAsync();
        Task<Guid> CreateProfileAsync(CreateUserProfileDto dto);
    }
}