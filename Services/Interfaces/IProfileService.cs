using PFL_API.Models.DTO;

namespace PFL_API.Services.Interfaces
{
    public interface IProfileService
    {
        // =======================
        // EXISTING METHODS (KEEP)
        // =======================
        Task CreateProfileAsync(CreateProfileRequest request);
        Task<ProfileDetailDto?> GetProfileDetailAsync(int userId);

        // =======================
        // NEW METHODS (ADD ONLY)
        // =======================

        /// <summary>
        /// Tạo profile duy nhất (ProfileId = 1)
        /// Chỉ tạo nếu chưa tồn tại
        /// </summary>
        Task AddSingleProfileAsync(CreateProfileRequest request);

        /// <summary>
        /// Cập nhật profile duy nhất (ProfileId = 1)
        /// </summary>
        Task UpdateSingleProfileAsync(CreateProfileRequest request);
    }
}
