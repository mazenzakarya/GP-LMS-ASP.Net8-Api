using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class AddPaymentCycleDto
    {
        public int GroupPaymentCycleId { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int MonthNumber { get; set; }
        public int SessionsCount { get; set; }


        public string CycleName { get; set; }

        public decimal Amount { get; set; }
        public bool IsActive { get; set; }



    }
}
