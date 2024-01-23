using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace API_TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class LoginController : ControllerBase
    {


        private DataContext _context;

        public LoginController(DataContext context)
        {
           _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName || u.Email == request.UserName);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect username or password.");
            }

            return Ok($"Welcome Back, {user.Email}!");
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512(passwordSalt))

            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }

        }


        [HttpPost("reset-password")]

        public async Task<IActionResult> ResetPassword(ResetPassword request)

        {
            //var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)// || user.ResetTokenExpires < DateTime.Now)

            {
                return BadRequest("Invalid Token");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;

            user.PasswordSalt = passwordSalt;

            user.PasswordResetToken = null;

            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();

            return Ok("Password Successfully reset!");
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512())

            {
                passwordSalt = hmac.Key;

                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        [HttpPost("forget-password")]

        public async Task<IActionResult> ForgetPassword(string email)

        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)

            {
                return BadRequest("User not found");
            }
            //user.PasswordResetToken = CreateRandomToken();
            //user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            return Ok("You may now reset your password.");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }


    }
}
