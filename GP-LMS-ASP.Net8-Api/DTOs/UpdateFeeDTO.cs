using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class UpdateFeeDTO
    {
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public FeeStatus Status { get; set; }
        public FeeType Type { get; set; }
        public string? Notes { get; set; }

        public int GroupId { get; set; }
        public int CourseId { get; set; }
        public int PaymentCycleId { get; set; }
    }
}