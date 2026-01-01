namespace PFL_API.Models.DTO
{
    public class CreateUserProfileDto
    {
        // User
        public string Email { get; set; }

        // Profile
        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CareerObjective { get; set; }

    }
}
