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

        public async Task UpdateFeeStatusesAsync(int studentId, int groupId)
        {
            var monthlyFees = await db.Fees
                .Where(f => f.Type == FeeType.Monthly
                            && f.StudentId == studentId
                            && f.GroupId == groupId
                            && !f.IsDeleted)
                .OrderBy(f => f.Date)
                .ToListAsync();

            var feesGroupedByMonth = monthlyFees
                .GroupBy(f => new { f.Date.Year, f.Date.Month })
                .ToList();

            foreach (var monthGroup in feesGroupedByMonth)
            {
                var year = monthGroup.Key.Year;
                var month = monthGroup.Key.Month;

                int attendanceCount = await db.Attendances
                    .Where(a => a.StudentId == studentId
                             && a.GroupId == groupId
                             && a.Date.Year == year
                             && a.Date.Month == month
                             && !a.IsExcepctionSession)
                    .CountAsync();

                var lastFee = monthGroup.OrderByDescending(f => f.Date).FirstOrDefault();
                if (lastFee == null)
                    continue;

                DateTime periodEnd = new DateTime(year, month, 1).AddMonths(1);

                if (attendanceCount >= 4 || DateTime.UtcNow >= periodEnd)
                {
                    if (lastFee.Status != FeeStatus.Unpaid)
                    {
                        lastFee.Status = FeeStatus.Unpaid;
                        lastFee.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            await db.SaveChangesAsync();
        }
    }
}