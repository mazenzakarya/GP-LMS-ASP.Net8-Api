
namespace GP_LMS_ASP.Net8_Api.Models
{
    public enum PaymentStatus
    {
        Unpaid = 0,
        PartiallyPaid = 1,
        Paid = 2
    }

    public class GroupPaymentCycle
    {
        public int GroupPaymentCycleId { get; set; }

        public int GroupId { get; set; }
        public Groups Group { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int MonthNumber { get; set; }
        public int SessionsCount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public bool IsDeleted { get; set; }
    }
}
