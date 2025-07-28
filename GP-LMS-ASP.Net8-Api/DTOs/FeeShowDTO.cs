using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class FeeShowDTO
    {
        public int FeeId { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal NetAmount => Amount - (Discount ?? 0);
        public FeeType Type { get; set; }
        public FeeStatus Status { get; set; }
        public string? Notes { get; set; }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }

        public int GroupId { get; set; }
        public string? GroupName { get; set; }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime Date { get; set; }
    }
}