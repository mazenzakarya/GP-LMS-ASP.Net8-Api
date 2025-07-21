using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using GP_LMS_ASP.Net8_Api.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyContext db;
        private readonly IConfiguration _config;

        public AuthController(MyContext context, IConfiguration config)
        {
            db = context;
            _config = config;
        }

        [Authorize] 
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var requesterRole = User.FindFirst(ClaimTypes.Role).Value;

            if (requesterRole == null)
                return Unauthorized("Invalid token");

            if (dto.Role == "Admin" && requesterRole != "Admin")
                return Forbid("Only Admin can create another Admin.");

            if ((requesterRole == "Manager" || requesterRole == "Secretary") && dto.Role != "Student")
                return Forbid("You are only allowed to create Student accounts.");

            if (requesterRole == "Instructor" || requesterRole == "Student")
                return Forbid("You are not allowed to register users.");

            if (db.Users.Any(u => u.Username == dto.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                Name = dto.Name,
                Role = dto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            db.Users.Add(user);
            db.SaveChanges();

            return Ok("User registered");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var user = db.Users.SingleOrDefault(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ThisIsAVeryStrongSecretKeyForJwt123asdsae@@ad...!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), 
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt, user });
        }
    }
}