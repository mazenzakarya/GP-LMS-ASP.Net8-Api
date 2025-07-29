using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs.CourseSubjectElements;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseSubjectElementsController : ControllerBase
    {
        private readonly MyContext _context;

        public CourseSubjectElementsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/CourseSubjectElements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseSubjectElementDto>>> GetElements()
        {
            var elements = await _context.CourseSubjectElements
                .Where(e => !e.IsDeleted)
                .Select(e => new CourseSubjectElementDto
                {
                    CourseSubjectElementsId = e.CourseSubjectElementsId,
                    SubjectId = e.SubjectId,
                    CourseId = e.CourseId,
                    GroupId = e.GroupId,
                    Description = e.Description,
                    Date = e.Date,
                    DueDate = e.DueDate,
                    TotalMarks = e.TotalMarks
                })
                .ToListAsync();

            return Ok(elements);
        }

        // GET: api/CourseSubjectElements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseSubjectElementDto>> GetElement(int id)
        {
            var element = await _context.CourseSubjectElements
                .Where(e => e.CourseSubjectElementsId == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (element == null)
                return NotFound();

            var dto = new CourseSubjectElementDto
            {
                CourseSubjectElementsId = element.CourseSubjectElementsId,
                SubjectId = element.SubjectId,
                CourseId = element.CourseId,
                GroupId = element.GroupId,
                Description = element.Description,
                Date = element.Date,
                DueDate = element.DueDate,
                TotalMarks = element.TotalMarks
            };

            return Ok(dto);
        }

        // POST: api/CourseSubjectElements
        [HttpPost]
        public async Task<ActionResult<CourseSubjectElementDto>> CreateElement(CourseSubjectElementCreateDto dto)
        {
            // تأكد من وجود البيانات المرتبطة
            var subjectExists = await _context.CourseSubjects.AnyAsync(s => s.CourseSubjectId == dto.SubjectId && !s.IsDeleted);
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == dto.CourseId && !c.IsDeleted);
            var groupExists = await _context.Groups.AnyAsync(g => g.GroupsId == dto.GroupId);

            if (!subjectExists || !courseExists || !groupExists)
                return BadRequest("Invalid Course / Subject / Group ID");

            var element = new CourseSubjectElements
            {
                SubjectId = dto.SubjectId,
                CourseId = dto.CourseId,
                GroupId = dto.GroupId,
                Description = dto.Description,
                Date = dto.Date,
                DueDate = dto.DueDate,
                TotalMarks = dto.TotalMarks,
                IsDeleted = false
            };

            _context.CourseSubjectElements.Add(element);
            await _context.SaveChangesAsync();

            var result = new CourseSubjectElementDto
            {
                CourseSubjectElementsId = element.CourseSubjectElementsId,
                SubjectId = element.SubjectId,
                CourseId = element.CourseId,
                GroupId = element.GroupId,
                Description = element.Description,
                Date = element.Date,
                DueDate = element.DueDate,
                TotalMarks = element.TotalMarks
            };

            return CreatedAtAction(nameof(GetElement), new { id = element.CourseSubjectElementsId }, result);
        }

        // PUT: api/CourseSubjectElements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateElement(int id, CourseSubjectElementUpdateDto dto)
        {
            var element = await _context.CourseSubjectElements.FindAsync(id);
            if (element == null || element.IsDeleted)
                return NotFound();

            element.SubjectId = dto.SubjectId;
            element.CourseId = dto.CourseId;
            element.GroupId = dto.GroupId;
            element.Description = dto.Description;
            element.Date = dto.Date;
            element.DueDate = dto.DueDate;
            element.TotalMarks = dto.TotalMarks;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/CourseSubjectElements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElement(int id)
        {
            var element = await _context.CourseSubjectElements.FindAsync(id);
            if (element == null || element.IsDeleted)
                return NotFound();

            element.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/CourseSubjectElements/pending/{studentId}
        [HttpGet("pending/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseSubjectElementDto>>> GetUnGradedElementsForStudent(int studentId)
        {
            var groupIds = await _context.StudentGroups
                .Where(sg => sg.StudentId == studentId)
                .Select(sg => sg.GroupId)
                .ToListAsync();

            if (!groupIds.Any())
                return NotFound("Student is not assigned to any group.");

            var elements = await _context.CourseSubjectElements
                .Where(e => groupIds.Contains(e.GroupId) &&
                            !_context.Grades.Any(g => g.ElementId == e.CourseSubjectElementsId && g.StudentId == studentId))
                .Select(e => new CourseSubjectElementDto
                {
                    CourseSubjectElementsId = e.CourseSubjectElementsId,
                    SubjectId = e.SubjectId,
                    CourseId = e.CourseId,
                    GroupId = e.GroupId,
                    Description = e.Description,
                    Date = e.Date,
                    DueDate = e.DueDate,
                    TotalMarks = e.TotalMarks
                })
                .ToListAsync();

            return Ok(elements);
        }
    }
}