using System.ComponentModel.DataAnnotations;

namespace API_TimeTracker.Models
{
    public class UserRegisterRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(8, ErrorMessage = "Please enter atleast 8 Characters")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}
