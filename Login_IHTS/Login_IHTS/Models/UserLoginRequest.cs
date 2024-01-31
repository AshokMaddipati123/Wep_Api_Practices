using System.ComponentModel.DataAnnotations;

namespace API_TimeTracker.Models
{
    public class UserLoginRequest
    {
         
         public string UserName { get; set; }

            [Required]
         public string Password { get; set; }
        




    }
}
