namespace case1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Doctor";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
