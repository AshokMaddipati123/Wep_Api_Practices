using API_TimeTracker.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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
            if (_context.USERDETAILS.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }

           
            var encryptedPassword = PasswordUtility.HashPassword(request.Password);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = encryptedPassword,
                
            };

            _context.USERDETAILS.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User successfully Created!");
        }

    }
}
