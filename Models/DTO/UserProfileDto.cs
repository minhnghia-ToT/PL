namespace PFL_API.Models.DTO
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ProfileId { get; set; }
        public string FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CareerObjective { get; set; }
    }
}
