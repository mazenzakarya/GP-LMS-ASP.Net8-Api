using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Helpers;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly MyContext db;

        public AttendanceController(MyContext db)
        {
            this.db = db;
        }

        //mark attendace
        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance([FromBody] List<AttendanceDTO> attendanceList)
        {
            foreach (var item in attendanceList)
            {
                var existing = await db.Attendances
                    .FirstOrDefaultAsync(a => a.StudentId == item.StudentId
                                            && a.GroupId == item.GroupId
                                            && a.Date.Date == item.Date.Date
                                            && !a.IsExcepctionSession);

                if (existing != null)
                {
                    existing.Status = item.Status;
                }
                else
                {
                    db.Attendances.Add(new Attendance
                    {
                        StudentId = item.StudentId,
                        GroupId = item.GroupId,
                        Date = item.Date.Date,
                        Status = item.Status,
                        IsExcepctionSession = item.IsExcepctionSession
                    });
                }
            }

            await db.SaveChangesAsync();

            var updater = new FeeStatusUpdater(db);
            var distinctStudents = attendanceList
                .Select(a => new { a.StudentId, a.GroupId })
                .Distinct();

            foreach (var s in distinctStudents)
            {
                await updater.UpdateFeeStatusesAsync(s.StudentId, s.GroupId);
            }

            return Ok(new { message = "Attendance marked successfully." });
        }

        //get attendance by group and date
        [HttpGet("{groupId}/date/{date}")]
        public async Task<IActionResult> GetGroupAttendanceByDate(int groupId, DateTime date)
        {
            var attendance = await db.Attendances
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

        //get full report with group id
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetGroupAttendance(int groupId)
        {
            var attendance = await db.Attendances
                .Where(a => a.GroupId == groupId)
                .Select(a => new
                {
                    a.StudentId,
                    a.Student.Name,
                    a.Student.Username,
                    a.Status,
                    a.Date.Date,
                    GroupName = a.Group.Name
                })
                .ToListAsync();

            return Ok(attendance);
        }

        // Fix for CS0833: An anonymous type cannot have multiple properties with the same name
        // The issue occurs because `a.Date.Date` is used multiple times in the anonymous type, causing duplicate property names.

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(int studentId)
        {
            var attendance = await db.Attendances
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .Select(a => new
                {
                    a.Date, // Keep the original Date property
                    GroupName = a.Group.Name, // Rename property to avoid conflict
                    Status = a.Status.ToString()
                })
                .ToListAsync();

            return Ok(attendance);
        }

        //get student ExcepctionSession attendance -may be not needed now-
        [HttpGet("student/{studentId}/ExcepctionSession")]
        public async Task<IActionResult> GetStudentMakeupAttendance(int studentId)
        {
            var ex = await db.Attendances
                .Where(a => a.StudentId == studentId && a.IsExcepctionSession)
                .OrderByDescending(a => a.Date)
                .Select(a => new
                {
                    a.Date,
                    a.Group.Name,
                    a.Status
                })
                .ToListAsync();

            return Ok(ex);
        }

        //full report with ex session
        [HttpGet("student-absenbt-report/{studentId}")]
        public async Task<IActionResult> GetFullReportStudentAttendance(int studentId)
        {
            var attendance = await db.Attendances
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .Select(a => new
                {
                    a.Date,
                    GroupName = a.Group.Name,
                    a.Status,
                    a.IsExcepctionSession
                })
                .ToListAsync();

            return Ok(attendance);
        }
    }
}