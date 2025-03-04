namespace WinklaaBlog.DTO
{
    public partial class UserToAdd
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
