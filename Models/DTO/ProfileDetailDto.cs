namespace PFL_API.Models.DTO
{
    public class ProfileDetailDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;

        public int ProfileId { get; set; }
        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CareerObjective { get; set; }
        public string? Summary { get; set; }

        public List<EducationDto> Educations { get; set; } = new();
        public List<ProjectDto> Projects { get; set; } = new();
    }
}
