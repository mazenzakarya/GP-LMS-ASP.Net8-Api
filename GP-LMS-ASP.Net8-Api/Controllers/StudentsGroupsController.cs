using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsGroupsController : ControllerBase
    {
        private readonly MyContext _context;

        public StudentsGroupsController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("{groupId}/students")]
        public async Task<ActionResult<IEnumerable<GroupStudentDTO>>> GetStudentsInGroup(int groupId)
        {
            var students = await _context.StudentGroups
                .Where(sg => sg.GroupId == groupId
                             && sg.Student.Role == "Student"
                             && !sg.Student.IsDeleted)
                .Select(sg => new GroupStudentDTO
                {
                    UserId = sg.Student.UserId,
                    Name = sg.Student.Name,
                    Gender = sg.Student.Gender,
                    PhoneNumber = sg.Student.PhoneNumber,
                    DOB = sg.Student.DOB,
                    Address = sg.Student.Address,
                    PaymentStatus = sg.PaymentStatus,

                    // Parent Info
                    ParentId = sg.Student.ParentId,
                    FatherName = sg.Student.Parent.FatherName,
                    MotherName = sg.Student.Parent.MotherName,
                    ParentPhoneNumber = sg.Student.Parent.PhoneNumber
                })
                .ToListAsync();

            if (students == null || students.Count == 0)
                return NotFound($"No students found in group ID {groupId}");

            return Ok(students);
        }

        //[HttpPost("assign-students")]
        //public async Task<IActionResult> AssignStudentsToGroup([FromBody] AssginStudentsGroupDTO dto)
        //{
        //    if (!await _context.Groups.AnyAsync(g => g.GroupsId == dto.GroupId))
        //        return NotFound($"Group with ID {dto.GroupId} not found.");

        //    var validStudents = await _context.Users
        //        .Where(u => dto.StudentIds.Contains(u.UserId) && u.Role == "Student" && !u.IsDeleted)
        //        .ToListAsync();

        //    if (!validStudents.Any())
        //        return BadRequest("No valid students found to assign.");

        //    var existingAssignments = await _context.StudentGroups
        //        .Where(sg => sg.GroupId == dto.GroupId && dto.StudentIds.Contains(sg.StudentId))
        //        .ToListAsync();

        //    var newAssignments = validStudents
        //        .Where(s => !existingAssignments.Any(ea => ea.StudentId == s.UserId))
        //        .Select(s => new StudentGroup
        //        {
        //            GroupId = dto.GroupId,
        //            StudentId = s.UserId,
        //            PaymentStatus = "Unpaid" // or default
        //        })
        //        .ToList();

        //    _context.StudentGroups.AddRange(newAssignments);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Students assigned successfully", assignedCount = newAssignments.Count });
        //}

        /*------------------------------------------*/

        [HttpPost("assign-student")]
        public async Task<IActionResult> AssignStudentToGroup([FromBody] AssginStudentsGroupDTO dto)
        {
            if (!await _context.Groups.AnyAsync(g => g.GroupsId == dto.GroupId))
                return NotFound($"Group with ID {dto.GroupId} not found.");

            var student = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == dto.StudentId && u.Role == "Student" && !u.IsDeleted);

            if (student == null)
                return BadRequest("Student not found or invalid.");

            var alreadyAssigned = await _context.StudentGroups
                .AnyAsync(sg => sg.GroupId == dto.GroupId && sg.StudentId == dto.StudentId);

            if (alreadyAssigned)
                return BadRequest("Student already assigned to this group.");

            var assignment = new StudentGroup
            {
                GroupId = dto.GroupId,
                StudentId = dto.StudentId,
                PaymentStatus = "Unpaid"
            };

            _context.StudentGroups.Add(assignment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Student assigned successfully" });
        }

        /*--------------------------------------------------------*/

        [HttpGet("student/{studentId}/group-details")]
        public async Task<IActionResult> GetStudentGroupDetails(int studentId)
        {
            var groupInfo = await _context.StudentGroups
                .Where(sg => sg.StudentId == studentId && !sg.Group.IsDeleted)
                .Select(sg => new
                {
                    sg.Group.GroupsId,
                    Name = sg.Group.Name,
                    CourseId = sg.Group.CourseId,
                    CourseName = sg.Group.Course.Name,
                    TeacherId = sg.Group.TeacherId,
                    TeacherName = sg.Group.Teacher.Name,
                    Amount = sg.Group.Amount,
                    LevelStartDate = sg.Group.LevelStartDate,
                    LevelEndDate = sg.Group.LevelEndDate,
                    NextPaymentDate = sg.Group.NextPaymentDate,
                    IsDeleted = sg.Group.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (groupInfo == null)
                return NotFound("No group found for this student.");

            return Ok(groupInfo);
        }

        //get group id and course id with student id

        [HttpGet("student/{studentId}/group-class-details")]
        public async Task<IActionResult> GetStudentGroupClassDetails(int studentId)
        {
            var groupClassInfo = await _context.StudentGroups
                .Include(sg => sg.Group)
                    .ThenInclude(g => g.Course)
                .Where(sg => sg.StudentId == studentId)
                .Select(sg => new
                {
                    GroupId = sg.GroupId,
                    GroupName = sg.Group.Name,
                    CourseId = sg.Group.CourseId,
                    CourseName = sg.Group.Course.Name
                })
                .FirstOrDefaultAsync();

            if (groupClassInfo == null)
            {
                return NotFound("Student not found in any group.");
            }

            return Ok(groupClassInfo);
        }
    }
}