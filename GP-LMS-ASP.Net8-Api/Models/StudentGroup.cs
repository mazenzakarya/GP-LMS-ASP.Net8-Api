using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class StudentGroup
    {
        public int StudentGroupId { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public User Student { get; set; }

        public int GroupId { get; set; }
        public Groups Group { get; set; }

        public string PaymentStatus { get; set; } // Paid, Unpaid
    }
}
