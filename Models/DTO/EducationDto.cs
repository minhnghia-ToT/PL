namespace PFL_API.Models.DTO
{
    public class EducationDto
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; } = null!;
        public string? Major { get; set; }
        public string? Degree { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
