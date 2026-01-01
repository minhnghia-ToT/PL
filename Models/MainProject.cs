namespace PFL_API.Models
{
    public class MainProject
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }

        public string Title { get; set; } = null!;
        public string? Overview { get; set; }
        public string? Contribution { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
