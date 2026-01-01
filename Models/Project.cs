namespace PFL_API.Models
{
    public class Project
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Technologies { get; set; }
        public string? Role { get; set; }
        public string? ProjectUrl { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
