using System.ComponentModel.DataAnnotations;

namespace MVC_TimeTracker.Models
{
    public class UserLoginViewModel
    {

        public string UserName { get; set; }


        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
