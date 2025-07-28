using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly MyContext _context;

        public AttendanceController(MyContext context)
        {
            _context = context;
        }

        //mark attendace
        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance([FromBody] List<AttendanceDTO> attendanceList)
        {
            foreach (var item in attendanceList)
            {
                var existing = await _context.Attendances
                    .FirstOrDefaultAsync(a => a.StudentId == item.StudentId && a.GroupId == item.GroupId && a.Date.Date == item.Date.Date);

                if (existing != null)
                {
                    existing.Status = item.Status;
                }
                else
                {
                    _context.Attendances.Add(new Attendance
                    {
                        StudentId = item.StudentId,
                        GroupId = item.GroupId,
                        Date = item.Date.Date,
                        Status = item.Status
                    });
                }
            }

            await _context.SaveChangesAsync();

            var studentId = attendanceList[0].StudentId;
            var groupId = attendanceList[0].GroupId;

            var sessionsCount = await _context.Attendances
                .Where(a => a.StudentId == studentId && a.GroupId == groupId && a.Date.Date == DateTime.Now.Date)
                .CountAsync();

            var isFourthSession = sessionsCount % 4 == 0;
            if (isFourthSession)
            {
                await _context.StudentGroups.Where(sg => sg.GroupId == groupId).ExecuteUpdateAsync(setters => setters
                .SetProperty(sg => sg.PaymentStatus, "unpaid"));
            }

            return Ok("Attendance marked successfully.");
        }

        //get attendance by group and date
        [HttpGet("{groupId}/date/{date}")]
        public async Task<IActionResult> GetGroupAttendanceByDate(int groupId, DateTime date)
        {
            var attendance = await _context.Attendances
                .Where(a => a.GroupId == groupId && a.Date.Date == date.Date)
                .Select(a => new
                {
                    a.StudentId,
                    a.Student.Name,
                    a.Status
                })
                .ToListAsync();

            return Ok(attendance);
        }

        //get student attendace report
        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(int studentId)
        {
            var attendance = await _context.Attendances
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .Select(a => new
                {
                    a.Date,
                    a.Group.Name,
                    a.Status
                })
                .ToListAsync();

            return Ok(attendance);
        }
    }
}