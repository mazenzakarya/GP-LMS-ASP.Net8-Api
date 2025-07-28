using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class CreateFeeDTO
    {
        public int StudentId { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public FeeType Type { get; set; }
        public string? Notes { get; set; }
        public decimal NetAmount => Amount - (Discount ?? 0);
        public int GroupId { get; set; }
        public int CourseId { get; set; }
    }
}