using GP_LMS_ASP.Net8_Api.DTOs.Grade;
using GP_LMS_ASP.Net8_Api.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GP_LMS_ASP.Net8_Api.Context;
using Microsoft.EntityFrameworkCore;
using GP_LMS_ASP.Net8_Api.DTOs.AssignGroupTeacher;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : ControllerBase
    {
        private readonly MyContext _context;

        public GradesController(MyContext context)
        {
            _context = context;
        }

        // GET: api/Grades/student/5
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGradesByStudent(int studentId)
        {
            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId)
                .Include(g => g.Student)
                .Include(g => g.Element)
                .Select(g => new GradeDto
                {
                    GradeId = g.GradeId,
                    StudentId = g.StudentId,
                    StudentName = g.Student.Name,
                    CourseId = g.CourseId,
                    SubjectId = g.SubjectId,
                    ElementId = g.ElementId,
                    ElementDescription = g.Element.Description,
                    Score = g.Score,
                    Notes = g.Notes,
                    GradedAt = g.GradedAt
                })
                .ToListAsync();

            return Ok(grades);
        }

        // POST: api/Grades
        [HttpPost]
        public async Task<ActionResult<GradeDto>> AddGrade(GradeCreateDto dto)
        {
            //cheak IDs
            var student = await _context.Users.FindAsync(dto.StudentId);
            var courseExists = await _context.Courses.AnyAsync(c => c.CourseId == dto.CourseId && !c.IsDeleted);
            var subjectExists = await _context.CourseSubjects.AnyAsync(s => s.CourseSubjectId == dto.SubjectId && !s.IsDeleted);
            var element = await _context.CourseSubjectElements.FindAsync(dto.ElementId);

            if (student == null || !courseExists || !subjectExists || element == null || element.IsDeleted)
                return BadRequest("Invalid related data.");

            var grade = new Grade
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                SubjectId = dto.SubjectId,
                ElementId = dto.ElementId,
                Score = dto.Score,
                Notes = dto.Notes,
                GradedAt = DateTime.UtcNow
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            var result = new GradeDto
            {
                GradeId = grade.GradeId,
                StudentId = student.UserId,
                StudentName = student.Name,
                CourseId = grade.CourseId,
                SubjectId = grade.SubjectId,
                ElementId = grade.ElementId,
                ElementDescription = element.Description,
                Score = grade.Score,
                Notes = grade.Notes,
                GradedAt = grade.GradedAt
            };

            return CreatedAtAction(nameof(GetGradesByStudent), new { studentId = grade.StudentId }, result);
        }

        //-----------------------assign teacher to group-------------
        [HttpPost("assign-teacher")]
        public async Task<IActionResult> AssignTeacherToGroup(AssignGroupTeacherDto dto)
        {
            var group = await _context.Groups.FindAsync(dto.GroupId);
            if (group == null || group.IsDeleted)
                return NotFound("Group not found.");

            var teacher = await _context.Users.FirstOrDefaultAsync(u => u.UserId == dto.TeacherId && u.Role == "Instructor");
            if (teacher == null)
                return BadRequest("Invalid teacher.");

            group.TeacherId = teacher.UserId;
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Teacher {teacher.Name} assigned to group {group.Name}" });
        }

        //-----------------------get group teacher-------------
        [HttpGet("teachers-with-groups")]
        public async Task<ActionResult<IEnumerable<TeacherWithGroupsDto>>> GetTeachersWithGroups()
        {
            var teachersWithGroups = await _context.Users
                .Where(u => u.Role == "Instructor" && !u.IsDeleted)
                .Select(t => new TeacherWithGroupsDto
                {
                    TeacherId = t.UserId,
                    TeacherName = t.Name,
                    GroupNames = t.StudentGroups
                        .Where(sg => sg.Group != null)
                        .Select(sg => sg.Group.Name)
                        .Distinct()
                        .ToList()
                })
                .ToListAsync();

            if (teachersWithGroups.All(t => t.GroupNames.Count == 0))
            {
                teachersWithGroups = await _context.Groups
                    .Include(g => g.Teacher)
                    .GroupBy(g => g.Teacher)
                    .Select(g => new TeacherWithGroupsDto
                    {
                        TeacherId = g.Key.UserId,
                        TeacherName = g.Key.Name,
                        GroupNames = g.Select(x => x.Name).ToList()
                    })
                    .ToListAsync();
            }

            return Ok(teachersWithGroups);
        }

        //-----------------------get group students by teacher id-------------
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<GroupBriefDto>>> GetGroupsByTeacher(int teacherId)
        {
            var groups = await _context.Groups
                .Where(g => g.TeacherId == teacherId)
                .Include(g => g.Course)
                .Select(g => new GroupBriefDto
                {
                    GroupId = g.GroupsId,
                    GroupName = g.Name,
                    CourseName = g.Course.Name,
                    StartDate = g.LevelStartDate
                })
                .ToListAsync();

            return Ok(groups);
        }

        //new api to get grades by group and student id
        // GET: api/Grades/student/5/group/3
        [HttpGet("student/{studentId}/group/{groupId}")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGradesByStudentInGroup(int studentId, int groupId)
        {
            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId && g.Element.GroupId == groupId)
                .Include(g => g.Student)
                .Include(g => g.Element)
                .Include(g => g.Subject)
                .Select(g => new GradeDto
                {
                    GradeId = g.GradeId,
                    StudentId = g.StudentId,
                    StudentName = g.Student.Name,
                    CourseId = g.CourseId,
                    SubjectId = g.SubjectId,
                    ElementId = g.ElementId,
                    ElementDescription = g.Element.Description,
                    Score = g.Score,
                    Notes = g.Notes,
                    GradedAt = g.GradedAt
                })
                .ToListAsync();

            return Ok(grades);
        }
    }
}