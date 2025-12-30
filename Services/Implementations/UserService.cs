using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Models;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;
using System.Text;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserProfileDto>> GetAllUsersWithProfilesAsync()
    {
        return await _context.Users
            .Include(u => u.Profile)
            .Select(u => new UserProfileDto
            {
                UserId = u.Id,
                Email = u.Email,
                CreatedAt = u.CreatedAt,

                ProfileId = u.Profile != null ? u.Profile.Id : Guid.Empty,
                FullName = u.Profile != null ? u.Profile.FullName : null,
                Dob = u.Profile != null ? u.Profile.Dob : null,
                Phone = u.Profile != null ? u.Profile.Phone : null,
                Address = u.Profile != null ? u.Profile.Address : null,
                CareerObjective = u.Profile != null ? u.Profile.CareerObjective : null
            })
            .ToListAsync();
    }
    public async Task<Guid> CreateProfileAsync(CreateUserProfileDto dto)
    {
        // 1️⃣ Check email tồn tại
        var existedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existedUser != null)
        {
            throw new Exception("Email already exists");
        }

        // 2️⃣ Tạo User
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // 3️⃣ Tạo Profile
        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            FullName = dto.FullName,
            Dob = dto.Dob,
            Phone = dto.Phone,
            Address = dto.Address,
            CareerObjective = dto.CareerObjective
        };

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();

        return profile.Id;
    }
}