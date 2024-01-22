using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Login_IHTS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

      
        private DataContext _context;

        public UserController(DataContext context)

        {

            _context = context;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register( UserRegisterRequest request)
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

        [HttpPost("reset-password")]

        public async Task<IActionResult> ReserPassword(ResetPassword request)

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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)

        {
            using (var hmac = new HMACSHA512(passwordSalt))

            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }

        }

        //private string CreateRandomToken()

        //{

        //    return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        //}
        [HttpGet("projects")]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _context.Projects.ToList();
                return Ok(projects);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("locations")]
        public IActionResult GetLocations()
        {
            try
            {
                var locations = _context.Locations.ToList();
                return Ok(locations);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("SaveTask")]
        public IActionResult SaveTask([FromBody] TaskModel model)
        {

            try
            {
                _context.Tasks.Add(model);
                _context.SaveChanges();

                // Returning the list of all tasks after saving
                var tasks = _context.Tasks.ToList();
                return Ok(tasks);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
