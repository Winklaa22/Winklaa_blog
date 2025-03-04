namespace WinklaaBlog.DTO
{
    public class UserForLoginConfirmationDto
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public UserForLoginConfirmationDto()
        {
            PasswordHash ??= [];

            PasswordSalt ??= [];
        }
    }
}
