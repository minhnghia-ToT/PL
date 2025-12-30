using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PFL_API.Models
{
    public class User
    {
        public Guid Id { get; set; }

       /* public string PasswordHash { get; set; }*/
        public string? Email { get; set; }   // chỉ để liên hệ, không dùng login
        public DateTime CreatedAt { get; set; }

        public Profile? Profile { get; set; }
    }
}
