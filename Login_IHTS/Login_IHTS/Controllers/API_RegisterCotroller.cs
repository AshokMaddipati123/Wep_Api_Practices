using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace API_TimeTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class API_RegisterCotroller : ControllerBase
    {
        private DataContext _context;

        public API_RegisterCotroller(DataContext context)
        {
            _context = context;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }
            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User succesfully Created!");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512())

            {
                passwordSalt = hmac.Key;

                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

       
    }   
}
