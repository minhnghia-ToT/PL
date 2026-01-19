using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Models;
using PFL_API.Models.DTO;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // =======================
    // Get all users + profile
    // =======================
    public async Task<IEnumerable<UserProfileDto>> GetAllUsersWithProfilesAsync()
    {
        return await _context.Users
            .Include(u => u.Profile)
            .Select(u => new UserProfileDto
            {
                UserId = u.Id,
                Email = u.Email,
                CreatedAt = u.CreatedAt,

                ProfileId = u.Profile != null ? u.Profile.Id : null,
                FullName = u.Profile != null ? u.Profile.FullName : null,
                Dob = u.Profile != null ? u.Profile.Dob : null,
                Phone = u.Profile != null ? u.Profile.Phone : null,
                Address = u.Profile != null ? u.Profile.Address : null,
                CareerObjective = u.Profile != null ? u.Profile.CareerObjective : null
            })
            .ToListAsync();
    }

    // =======================
    // Create USER + PROFILE
    // =======================
    public async Task<int> CreateUserProfileAsync(CreateUserProfileDto dto)
    {
        // 1. Create User
        var user = new User
        {
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); // lấy user.Id

        // 2. Create Profile
        var profile = new Profile
        {
            UserId = user.Id,
            FullName = dto.FullName,
            Dob = dto.Dob.HasValue
                ? DateOnly.FromDateTime(dto.Dob.Value)
                : (DateOnly?)null,
            Phone = dto.Phone,
            Address = dto.Address,
            CareerObjective = dto.CareerObjective
        };

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        return profile.Id;
    }

}
