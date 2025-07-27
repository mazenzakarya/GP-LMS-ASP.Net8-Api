using GP_LMS_ASP.Net8_Api.DTOs;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GP_LMS_ASP.Net8_Api.Context;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersCountController : ControllerBase
    {
        private readonly MyContext db;

        public UsersCountController(MyContext db)
        {
            this.db = db;
        }

        // GET: api/users/instructors/count
        [HttpGet("instructors/count")]
        public async Task<ActionResult<CountDto>> GetInstructorsCount()
        {
            var count = await db.Users
                .Where(u => u.Role == "Instructor")
                .CountAsync();

            var result = new CountDto
            {
                Role = "Instructor",
                Count = count
            };

            return Ok(result);
        }

        [HttpGet("admin/count")]
        public async Task<ActionResult<CountDto>> GetAdminCount()
        {
            var count = await db.Users
                .Where(u => u.Role == "Admin")
                .CountAsync();

            var result = new CountDto
            {
                Role = "Admin",
                Count = count
            };

            return Ok(result);
        }

        [HttpGet("student/count")]
        public async Task<ActionResult<CountDto>> GetStudentCount()
        {
            var count = await db.Users
                .Where(u => u.Role == "Student")
                .CountAsync();

            var result = new CountDto
            {
                Role = "Student",
                Count = count
            };

            return Ok(result);
        }

        [HttpGet("secrtary/count")]
        public async Task<ActionResult<CountDto>> GetSecrtaryCount()
        {
            var count = await db.Users
                .Where(u => u.Role == "Secrtary")
                .CountAsync();

            var result = new CountDto
            {
                Role = "Secrtary",
                Count = count
            };

            return Ok(result);
        }

        [HttpGet("manger/count")]
        public async Task<ActionResult<CountDto>> GetMangerCount()
        {
            var count = await db.Users
                .Where(u => u.Role == "Manger")
                .CountAsync();

            var result = new CountDto
            {
                Role = "Manger",
                Count = count
            };

            return Ok(result);
        }
    }
}