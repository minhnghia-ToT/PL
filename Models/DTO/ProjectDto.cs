namespace PFL_API.Models.DTO
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? Role { get; set; }
        public string? ProjectUrl { get; set; }
    }
}
