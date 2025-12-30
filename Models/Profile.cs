namespace PFL_API.Models
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CareerObjective { get; set; }

        public User User { get; set; } = null!;
    }
}
