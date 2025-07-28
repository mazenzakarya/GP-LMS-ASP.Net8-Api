using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs.Course;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly MyContext _context;

        public CoursesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _context.Courses
                .Where(c => !c.IsDeleted)
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(courses);
        }

        // GET: api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Where(c => c.CourseId == id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            var dto = new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description
            };

            return Ok(dto);
        }

        // POST: api/courses
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseCreateDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                IsDeleted = false
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            var resultDto = new CourseDto
            {
                CourseId = course.CourseId,
                Name = course.Name,
                Description = course.Description
            };

            return CreatedAtAction(nameof(GetCourse), new { id = course.CourseId }, resultDto);
        }

        // PUT: api/courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseUpdateDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null || course.IsDeleted)
                return NotFound();

            course.Name = dto.Name;
            course.Description = dto.Description;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null || course.IsDeleted)
                return NotFound();

            course.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //added new for chose course name and level description in create group
        // GET: api/courses/options
        [HttpGet("options")]
        public async Task<ActionResult<IEnumerable<CourseCreateDto>>> GetCourseOptions()
        {
            var courses = await _context.Courses
                .Where(c => !c.IsDeleted)
                .Select(c => new CourseCreateDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(courses);
        }
    }
}