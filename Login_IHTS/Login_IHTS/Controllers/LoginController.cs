using API_TimeTracker.Utilities;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class LoginController : ControllerBase
    {


        private DataContext _context;

        private string StoredUsername { get; set; }

        public LoginController(DataContext context)
        {
           _context = context;
        }
       


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.USERDETAILS.FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!PasswordUtility.VerifyPassword(request.Password, user.Password))
            {
                return BadRequest("Incorrect username or password.");
            }

          

            return Ok($"Welcome Back, {user.Email}!");
        }



        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword request)
        {
            var user = await _context.USERDETAILS.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Invalid Email");
            }

            
            var encryptedPassword = PasswordUtility.HashPassword(request.Password);

            user.Password = encryptedPassword;

            await _context.SaveChangesAsync();

            return Ok("Password Successfully reset!");
        }





    }
}
