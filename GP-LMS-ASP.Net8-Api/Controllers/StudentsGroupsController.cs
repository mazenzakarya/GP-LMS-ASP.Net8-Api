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
        public class StudentsController : ControllerBase
        {
            private readonly MyContext _context;

            public StudentsController(MyContext context)
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

            [HttpPost("assign-students")]
            public async Task<IActionResult> AssignStudentsToGroup([FromBody] AssginStudentsGroupDTO dto)
            {
                if (!await _context.Groups.AnyAsync(g => g.GroupsId == dto.GroupId))
                    return NotFound($"Group with ID {dto.GroupId} not found.");

                var validStudents = await _context.Users
                    .Where(u => dto.StudentIds.Contains(u.UserId) && u.Role == "Student" && !u.IsDeleted)
                    .ToListAsync();

                if (!validStudents.Any())
                    return BadRequest("No valid students found to assign.");

                var existingAssignments = await _context.StudentGroups
                    .Where(sg => sg.GroupId == dto.GroupId && dto.StudentIds.Contains(sg.StudentId))
                    .ToListAsync();

                var newAssignments = validStudents
                    .Where(s => !existingAssignments.Any(ea => ea.StudentId == s.UserId))
                    .Select(s => new StudentGroup
                    {
                        GroupId = dto.GroupId,
                        StudentId = s.UserId,
                        PaymentStatus = "Unpaid" // or default
                    })
                    .ToList();

                _context.StudentGroups.AddRange(newAssignments);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Students assigned successfully", assignedCount = newAssignments.Count });
            }


        }
    }
}