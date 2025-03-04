namespace WinklaaBlog.DTO
{
    public class UserForRegistrationDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? Username { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
