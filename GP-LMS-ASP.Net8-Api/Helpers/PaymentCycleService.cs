using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Helpers
{
    public class PaymentCycleService: IPaymentCycleService
    {
        private readonly MyContext db;

        public PaymentCycleService(MyContext _db)
        {
            _db = db; 
        }


        public async Task<GroupPaymentCycle> AddPaymentCycleAsync(AddPaymentCycleDto dto)
        {
            var cycle = new GroupPaymentCycle
            {
                GroupId = dto.GroupId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MonthNumber = dto.MonthNumber,
                SessionsCount = dto.SessionsCount,
                PaymentStatus = PaymentStatus.Unpaid
            };

            db.GroupPaymentCycles.Add(cycle);
            await db.SaveChangesAsync();

            return cycle;
        }

        public async Task MarkStudentsAsUnpaidJob()
        {
            var now = DateTime.UtcNow;

            var overdueCycles = await db.GroupPaymentCycles
                .Where(c => !c.IsDeleted &&
                            c.PaymentStatus != PaymentStatus.Paid &&
                            EF.Functions.DateDiffDay(c.StartDate, now) >= 28)
                .ToListAsync();

            foreach (var cycle in overdueCycles)
            {
                cycle.PaymentStatus = PaymentStatus.Unpaid;
            }

            await db.SaveChangesAsync();
        }
    }


}

