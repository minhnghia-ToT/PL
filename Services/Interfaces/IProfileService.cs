using PFL_API.Models.DTO;

namespace PFL_API.Services.Interfaces
{
    public interface IProfileService
    {
        Task CreateProfileAsync(CreateProfileRequest request);
        Task<ProfileDetailDto?> GetProfileDetailAsync(int userId);
    }
}
