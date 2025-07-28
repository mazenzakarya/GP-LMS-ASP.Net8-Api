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
                            && f.Status == FeeStatus.Paid
                            && f.StudentId == studentId
                            && f.GroupId == groupId)
                .ToListAsync();

            foreach (var fee in monthlyFees)
            {
                int attendanceCount = await db.Attendances
                    .Where(a => a.StudentId == fee.StudentId
                             && a.GroupId == fee.GroupId
                             && a.Date > fee.Date
                             && !a.IsExcepctionSession)
                    .CountAsync();

                if (attendanceCount % 4 == 0)
                {
                    fee.Status = FeeStatus.Unpaid;
                }
            }

            await db.SaveChangesAsync();
        }
    }
}