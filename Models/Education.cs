namespace PFL_API.Models
{
    public class Education
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public string SchoolName { get; set; } = null!;
        public string? Major { get; set; }
        public string? Degree { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
