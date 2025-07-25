using GP_LMS_ASP.Net8_Api.DTOs.CourseSubject;
using GP_LMS_ASP.Net8_Api.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GP_LMS_ASP.Net8_Api.Context;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseSubjectsController : ControllerBase
    {
        private readonly MyContext _context;

        public CourseSubjectsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/coursesubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseSubjectDto>>> GetCourseSubjects()
        {
            var subjects = await _context.CourseSubjects
                .Where(s => !s.IsDeleted)
                .Select(s => new CourseSubjectDto
                {
                    CourseSubjectId = s.CourseSubjectId,
                    Name = s.Name,
                    CourseId = s.CourseId
                })
                .ToListAsync();

            return Ok(subjects);
        }

        // GET: api/coursesubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseSubjectDto>> GetCourseSubject(int id)
        {
            var subject = await _context.CourseSubjects
                .Where(s => s.CourseSubjectId == id && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (subject == null)
                return NotFound();

            var dto = new CourseSubjectDto
            {
                CourseSubjectId = subject.CourseSubjectId,
                Name = subject.Name,
                CourseId = subject.CourseId
            };

            return Ok(dto);
        }

        // POST: api/coursesubjects
        [HttpPost]
        public async Task<ActionResult<CourseSubjectDto>> CreateCourseSubject(CourseSubjectCreateDto dto)
        {
            // Optional: cheak if course not exsit 
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == dto.CourseId && !c.IsDeleted);
            if (!courseExists)
                return BadRequest("Course not found.");

            var subject = new CourseSubject
            {
                Name = dto.Name,
                CourseId = dto.CourseId,
                IsDeleted = false
            };

            _context.CourseSubjects.Add(subject);
            await _context.SaveChangesAsync();

            var resultDto = new CourseSubjectDto
            {
                CourseSubjectId = subject.CourseSubjectId,
                Name = subject.Name,
                CourseId = subject.CourseId
            };

            return CreatedAtAction(nameof(GetCourseSubject), new { id = subject.CourseSubjectId }, resultDto);
        }

        // PUT: api/coursesubjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourseSubject(int id, CourseSubjectUpdateDto dto)
        {
            var subject = await _context.CourseSubjects.FindAsync(id);
            if (subject == null || subject.IsDeleted)
                return NotFound();

            // Optional: cheak if new course not exsit 
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == dto.CourseId && !c.IsDeleted);
            if (!courseExists)
                return BadRequest("Course not found.");

            subject.Name = dto.Name;
            subject.CourseId = dto.CourseId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/coursesubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseSubject(int id)
        {
            var subject = await _context.CourseSubjects.FindAsync(id);
            if (subject == null || subject.IsDeleted)
                return NotFound();

            subject.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
