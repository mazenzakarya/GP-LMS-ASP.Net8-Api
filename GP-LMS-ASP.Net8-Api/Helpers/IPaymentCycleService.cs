using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;

namespace GP_LMS_ASP.Net8_Api.Helpers
{
    public interface IPaymentCycleService
    {
        Task<GroupPaymentCycle> AddPaymentCycleAsync(AddPaymentCycleDto dto);

        Task MarkStudentsAsUnpaidJob();
    }
}