using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class AttendanceDTO
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public bool IsExcepctionSession { get; set; } = false;
    }
}