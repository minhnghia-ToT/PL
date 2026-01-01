namespace PFL_API.Models.DTO
{
    public class CreateProfileRequest
    {
        public int UserId { get; set; } // user đã tồn tại

        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CareerObjective { get; set; }
        public string? Summary { get; set; }

        public List<EducationCreateDto> Educations { get; set; } = new();
        public List<ProjectCreateDto> Projects { get; set; } = new();
    }

    public class EducationCreateDto
    {
        public string? SchoolName { get; set; }
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ProjectCreateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? Role { get; set; }
        public string? ProjectUrl { get; set; }
    }
}
