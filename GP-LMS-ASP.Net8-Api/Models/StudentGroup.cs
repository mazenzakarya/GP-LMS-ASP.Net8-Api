using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class StudentGroup
    {
        public int StudentGroupId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual User Student { get; set; }

        public int GroupId { get; set; }
        public virtual Groups Group { get; set; }

        public string PaymentStatus { get; set; } // Paid, Unpaid

        public DateTime LastPaymentDate { get; set; }
    }
}