using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class GetFeeDTO
    {
        public int FeeId { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public FeeType Type { get; set; }
        public FeeStatus Status { get; set; }
        public string CourseName { get; set; }
        public string GroupName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}