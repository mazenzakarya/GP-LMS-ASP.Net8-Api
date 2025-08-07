using GP_LMS_ASP.Net8_Api.Models;

using Microsoft.AspNetCore.Mvc;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyContext db;

        public UsersController(MyContext db)
        {
            this.db = db;
        }

        //auto generate username based on full name
        private async Task<string> GenerateUsernameAsync(string fullName)
        {
            string baseUsername = fullName.ToLower().Replace(" ", "");
            string username = baseUsername;
            int counter = 1;

            while (await db.Users.AnyAsync(u => u.Username == username))
            {
                username = $"{baseUsername}{counter}";
                counter++;
            }

            return username;
        }

        // Get All Users by role
        //using [FromQuery] to filter by role
        //using AsQueryable instead of .ToList() to allow filtering and selecting specific fields and performance optimization
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? role = null)
        {
            var query = db.Users.AsQueryable();

            if (!string.IsNullOrEmpty(role))
                query = query.Where(u => u.Role == role);

            var users = await query
                .Select(u => new UserReadDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Name = u.Name,
                    Role = u.Role,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Salary = u.Salary,
                    Gender = u.Gender,
                    Address = u.Address,
                    DOB = u.DOB
                })
                .ToListAsync();

            return Ok(users);
        }

        // Get Single User
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUser(int id)
        {
            var user = await db.Users
                .Where(u => u.UserId == id)
                .Select(u => new UserReadDTO
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    Name = u.Name,
                    Role = u.Role,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Salary = u.Salary,
                    Gender = u.Gender,
                    Address = u.Address,
                    DOB = u.DOB
                })
                .FirstOrDefaultAsync();

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto dto)
        {
            var currentUserRole = User.FindFirst("role")?.Value;

            if (currentUserRole == "Instructor")
                return Forbid("Instructors are not allowed to create users.");

            if (currentUserRole == "Secretary" &&
                dto.Role != "Student" &&
                dto.Role != "Instructor")
            {
                return Forbid("Secretaries can only create Students or Instructors.");
            }

            var username = await GenerateUsernameAsync(dto.Name);

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Name = dto.Name,
                Role = dto.Role,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Salary = dto.Salary,
                Gender = dto.Gender,
                Address = dto.Address,
                DOB = dto.DOB
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            var userDto = new UserReadDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Name = user.Name,
                Role = user.Role,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Salary = user.Salary,
                Gender = user.Gender,
                Address = user.Address,
                DOB = user.DOB
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, userDto);
        }

        //only Admin can update user

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO dto)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
                return NotFound("User not found.");

            user.Name = dto.Name ?? user.Name;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;
            user.Email = dto.Email ?? user.Email;
            user.Role = dto.Role ?? user.Role;
            user.Address = dto.Address ?? user.Address;
            user.Gender = dto.Gender ?? user.Gender;
            user.Salary = dto.Salary ?? user.Salary;
            user.DOB = dto.DOB ?? user.DOB;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await db.SaveChangesAsync();
            return Ok("User updated successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsDeleted = true;
            await db.SaveChangesAsync();

            return Ok("User Deleted.");
        }
    }
}