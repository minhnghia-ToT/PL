namespace PFL_API.Models
{
    public class User
    {
        public int Id { get; set; }   // Identity (INT)

        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Profile? Profile { get; set; }
    }
}
