using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Helpers
{
    public class FeeStatusUpdater
    {
        private readonly MyContext db;

        public FeeStatusUpdater(MyContext context)
        {
            db = context;
        }

        public async Task UpdateFeeStatusesAsync()
        {
            var monthlyFees = await db.Fees
                .Where(f => f.Type == FeeType.Monthly && f.Status == FeeStatus.Paid && !f.IsDeleted)
                .Include(f => f.Student)
                .ToListAsync();

            foreach (var fee in monthlyFees)
            {
                // Get number of attendances after the payment date
                int attendanceCount = await db.Attendances
                    .Where(a => a.StudentId == fee.StudentId
                             && a.GroupId == fee.GroupId
                             && a.Date > fee.Date)
                    .CountAsync();

                bool expiredByDate = DateTime.UtcNow > fee.Date.AddDays(28);
                bool expiredByAttendance = attendanceCount >= 4;

                if (expiredByDate || expiredByAttendance)
                {
                    fee.Status = FeeStatus.Unpaid;
                }
            }

            await db.SaveChangesAsync();
        }
    }
}