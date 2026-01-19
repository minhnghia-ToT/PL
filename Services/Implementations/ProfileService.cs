using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Models;
using PFL_API.Models.DTO;
using PFL_API.Services.Interfaces;
/*using PFL_API.Services.Interfaces;*/

public class ProfileService : IProfileService
{
    private readonly AppDbContext _context;

    public ProfileService(AppDbContext context)
    {
        _context = context;
    }

    // =======================
    // CREATE PROFILE
    // =======================
    public async Task CreateProfileAsync(CreateProfileRequest request)
    {
        // 1️⃣ Lấy user + profile
        var user = await _context.Users
            .Include(u => u.Profile)
                .ThenInclude(p => p.Educations)
            .Include(u => u.Profile)
                .ThenInclude(p => p.Projects)
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
            throw new Exception("User not found");

        Profile profile;

        // =======================
        // 2️⃣ CREATE nếu chưa có
        // =======================
        if (user.Profile == null)
        {
            profile = new Profile
            {
                UserId = user.Id
            };

            _context.Profiles.Add(profile);
        }
        // =======================
        // 3️⃣ UPDATE nếu đã có
        // =======================
        else
        {
            profile = user.Profile;

            // clear dữ liệu cũ (important)
            _context.Educations.RemoveRange(profile.Educations);
            _context.Projects.RemoveRange(profile.Projects);
        }

        // =======================
        // 4️⃣ Gán thông tin chung
        // =======================
        profile.FullName = request.FullName;
        profile.Dob = request.Dob;
        profile.Phone = request.Phone;
        profile.Address = request.Address;
        profile.CareerObjective = request.CareerObjective;
        profile.Summary = request.Summary;

        // =======================
        // 5️⃣ Educations
        // =======================
        if (request.Educations != null && request.Educations.Any())
        {
            profile.Educations = request.Educations.Select(e => new Education
            {
                SchoolName = e.SchoolName,
                Major = e.Major,
                Degree = e.Degree,
                StartDate = e.StartDate.HasValue
                    ? DateOnly.FromDateTime(e.StartDate.Value)
                    : null,
                EndDate = e.EndDate.HasValue
                    ? DateOnly.FromDateTime(e.EndDate.Value)
                    : null
            }).ToList();
        }

        // =======================
        // 6️⃣ Projects
        // =======================
        if (request.Projects != null && request.Projects.Any())
        {
            profile.Projects = request.Projects.Select(p => new Project
            {
                Name = p.Name,
                Description = p.Description,
                Technologies = p.Technologies,
                Role = p.Role,
                ProjectUrl = p.ProjectUrl
            }).ToList();
        }

        // =======================
        // 7️⃣ Save
        // =======================
        await _context.SaveChangesAsync();
    }


    // =======================
    // GET PROFILE DETAIL
    // =======================
    public async Task<ProfileDetailDto?> GetProfileDetailAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.Profile)
                .ThenInclude(p => p.Educations)
            .Include(u => u.Profile)
                .ThenInclude(p => p.Projects)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null || user.Profile == null)
            return null;

        return new ProfileDetailDto
        {
            UserId = user.Id,
            Email = user.Email,

            ProfileId = user.Profile.Id,
            FullName = user.Profile.FullName,
            Dob = user.Profile.Dob,
            Phone = user.Profile.Phone,
            Address = user.Profile.Address,
            CareerObjective = user.Profile.CareerObjective,
            Summary = user.Profile.Summary,

            Educations = user.Profile.Educations.Select(e => new EducationDto
            {
                Id = Guid.Empty, // or Guid.NewGuid() if you want a unique Guid
                SchoolName = e.SchoolName,
                Major = e.Major,
                Degree = e.Degree,
                StartDate = e.StartDate,
                EndDate = e.EndDate
            }).ToList(),

            Projects = user.Profile.Projects.Select(p => new ProjectDto
            {
                Id = Guid.Empty, // or generate a new Guid if needed, e.g., Guid.NewGuid()
                Name = p.Name,
                Description = p.Description,
                Technologies = p.Technologies,
                Role = p.Role,
                ProjectUrl = p.ProjectUrl
            }).ToList()
        };
    }
    // =======================
    // ADD SINGLE PROFILE
    // =======================
    public async Task AddSingleProfileAsync(CreateProfileRequest request)
    {
        var profile = await _context.Profiles
            .Include(p => p.Educations)
            .Include(p => p.Projects)
            .FirstOrDefaultAsync(p => p.Id == 1);

        if (profile != null)
            throw new Exception("Profile already exists");

        profile = new Profile
        {
            Id = 1,
            UserId = request.UserId,
            FullName = request.FullName,
            Dob = request.Dob,
            Phone = request.Phone,
            Address = request.Address,
            CareerObjective = request.CareerObjective,
            Summary = request.Summary
        };

        // Educations
        if (request.Educations != null && request.Educations.Any())
        {
            profile.Educations = request.Educations.Select(e => new Education
            {
                SchoolName = e.SchoolName,
                Major = e.Major,
                Degree = e.Degree,
                StartDate = e.StartDate.HasValue
                    ? DateOnly.FromDateTime(e.StartDate.Value)
                    : null,
                EndDate = e.EndDate.HasValue
                    ? DateOnly.FromDateTime(e.EndDate.Value)
                    : null
            }).ToList();
        }

        // Projects
        if (request.Projects != null && request.Projects.Any())
        {
            profile.Projects = request.Projects.Select(p => new Project
            {
                Name = p.Name,
                Description = p.Description,
                Technologies = p.Technologies,
                Role = p.Role,
                ProjectUrl = p.ProjectUrl
            }).ToList();
        }

        _context.Profiles.Add(profile);
        await _context.SaveChangesAsync();
    }
    // =======================
    // UPDATE SINGLE PROFILE
    // =======================
    public async Task UpdateSingleProfileAsync(CreateProfileRequest request)
    {
        var profile = await _context.Profiles
            .Include(p => p.Educations)
            .Include(p => p.Projects)
            .FirstOrDefaultAsync(p => p.Id == 1);

        if (profile == null)
            throw new Exception("Profile not found");

        // Clear dữ liệu cũ
        _context.Educations.RemoveRange(profile.Educations);
        _context.Projects.RemoveRange(profile.Projects);

        // Update thông tin chung
        profile.FullName = request.FullName;
        profile.Dob = request.Dob;
        profile.Phone = request.Phone;
        profile.Address = request.Address;
        profile.CareerObjective = request.CareerObjective;
        profile.Summary = request.Summary;

        // Educations
        if (request.Educations != null && request.Educations.Any())
        {
            profile.Educations = request.Educations.Select(e => new Education
            {
                SchoolName = e.SchoolName,
                Major = e.Major,
                Degree = e.Degree,
                StartDate = e.StartDate.HasValue
                    ? DateOnly.FromDateTime(e.StartDate.Value)
                    : null,
                EndDate = e.EndDate.HasValue
                    ? DateOnly.FromDateTime(e.EndDate.Value)
                    : null
            }).ToList();
        }

        // Projects
        if (request.Projects != null && request.Projects.Any())
        {
            profile.Projects = request.Projects.Select(p => new Project
            {
                Name = p.Name,
                Description = p.Description,
                Technologies = p.Technologies,
                Role = p.Role,
                ProjectUrl = p.ProjectUrl
            }).ToList();
        }

        await _context.SaveChangesAsync();
    }

}
