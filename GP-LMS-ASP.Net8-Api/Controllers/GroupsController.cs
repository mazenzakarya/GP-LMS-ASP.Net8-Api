using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
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
                        PaymentStatus = sg.PaymentStatus
                    })
                    .ToListAsync();

                if (students == null || students.Count == 0)
                    return NotFound($"No students found in group ID {groupId}");

                return Ok(students);
            }

        }
    }
}
