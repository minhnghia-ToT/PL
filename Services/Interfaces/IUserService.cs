using PFL_API.Models.DTO;

public interface IUserService
{
    Task<IEnumerable<UserProfileDto>> GetAllUsersWithProfilesAsync();

    Task<int> CreateUserProfileAsync(CreateUserProfileDto dto);
}
