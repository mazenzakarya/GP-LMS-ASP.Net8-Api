using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public enum AttendanceStatus
    {
        Present = 1,
        Absent = 2,
        Late = 3
    }
    public class Attendance
    {
        public int AttendanceId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public User Student { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Groups Group { get; set; }

        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
