namespace WinklaaBlog.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public string? RoleType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBanned { get; set; }
    }
}
