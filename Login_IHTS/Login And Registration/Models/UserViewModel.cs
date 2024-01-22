namespace Login_And_Registration.Models
{
    public class UserViewModel
    {

        public int Id { get; set; }



        public string UserName { get; set; }

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[32];

        public byte[] PasswordSalt { get; set; } = new byte[32];

        //public string? VerificationToken { get; set; }

       // public DateTime? VerifiedAt { get; set; }

        public string? PasswordResetToken { get; set; }

        //public DateTime? ResetTokenExpires { get; set; }



    }
}
