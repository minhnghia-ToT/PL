namespace PFL_API.Models.DTO
{
    public class UpdateProfileRequest
    {
        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CareerObjective { get; set; }
        public string? Summary { get; set; }

        public List<EducationDto>? Educations { get; set; }
        public List<ProjectDto>? Projects { get; set; }
    }
}
