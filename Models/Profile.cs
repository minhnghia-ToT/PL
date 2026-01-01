namespace PFL_API.Models
{
    public class Profile
    {
        public int Id { get; set; }          // Identity
        public int UserId { get; set; }      // FK -> User.Id

        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? CareerObjective { get; set; }
        public string? Summary { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
