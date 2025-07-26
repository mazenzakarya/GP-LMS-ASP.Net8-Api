using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private readonly MyContext db;

        public FeeController(MyContext context)
        {
            db = context;
        }

        private string? GetCurrentUsername()
        {
            return User.Identity?.Name ?? User.FindFirst("name")?.Value;
        }

        [HttpPost]
        public async Task<IActionResult> AddFee([FromBody] CreateFeeDTO dto)
        {
            var fee = new Fee
            {
                StudentId = dto.StudentId,
                Amount = dto.Amount,
                Discount = dto.Discount,
                Type = dto.Type,
                Status = FeeStatus.Paid,
                Notes = dto.Notes,
                GroupId = dto.GroupId,
                CourseId = dto.CourseId,
                PaymentCycleId = dto.PaymentCycleId,
                Date = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                UpdatedByUser = GetCurrentUsername()
            };

            db.Fees.Add(fee);
            await db.SaveChangesAsync();
            return Ok(fee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFees()
        {
            var fees = await db.Fees

    .Include(f => f.Student)
    .Include(f => f.Course)
    .Include(f => f.Group)
    .Select(f => new GetFeeDTO
    {
        FeeId = f.FeeId,
        Amount = f.Amount,
        Discount = f.Discount,
        Type = f.Type,
        Status = f.Status,
        CourseName = f.Course.Name,
        GroupName = f.Group.Name,
        StudentId = f.StudentId,
        StudentName = f.Student.Name
    })
    .ToListAsync();

            return Ok(fees);
        }

        //get by fee id for detailed view and necessary for update
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeeById(int id)
        {
            var fee = await db.Fees
                .Include(f => f.Student)
                .Include(f => f.Course)
                .Include(f => f.Group)
                .FirstOrDefaultAsync(f => f.FeeId == id);

            if (fee == null)
                return NotFound();

            var dto = new FeeShowDTO
            {
                FeeId = fee.FeeId,
                Amount = fee.Amount,
                Discount = fee.Discount,
                NetAmount = fee.NetAmount,
                Type = fee.Type,
                Status = fee.Status,
                Notes = fee.Notes,
                CourseId = fee.CourseId,
                CourseName = fee.Course?.Name,
                GroupId = fee.GroupId,
                GroupName = fee.Group?.Name,
                StudentId = fee.StudentId,
                StudentName = fee.Student?.Name,
                PaymentCycleId = fee.PaymentCycleId,
                Date = fee.Date,
                CreatedByUser = fee.UpdatedByUser,
                UpdatedAt = fee.UpdatedAt,
                CreatedAt = fee.CreatedAt
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFee(int id, [FromBody] UpdateFeeDTO dto)
        {
            var fee = await db.Fees.FirstOrDefaultAsync(f => f.FeeId == id);
            if (fee == null)
                return NotFound();

            fee.Amount = dto.Amount;
            fee.Discount = dto.Discount;
            fee.Status = dto.Status;
            fee.Type = dto.Type;
            fee.Notes = dto.Notes;
            fee.GroupId = dto.GroupId;
            fee.CourseId = dto.CourseId;
            fee.PaymentCycleId = dto.PaymentCycleId;

            fee.UpdatedAt = DateTime.UtcNow;
            fee.UpdatedByUser = GetCurrentUsername();

            await db.SaveChangesAsync();
            return Ok(fee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteFee(int id)
        {
            var fee = await db.Fees.FindAsync(id);
            if (fee == null)
                return NotFound();

            fee.IsDeleted = true;
            fee.UpdatedAt = DateTime.UtcNow;
            fee.UpdatedByUser = GetCurrentUsername();

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetFeeReport()
        {
            var fees = await db.Fees.ToListAsync();

            var report = new
            {
                TotalAmount = fees.Sum(f => f.Amount),
                TotalDiscount = fees.Sum(f => f.Discount ?? 0),
                TotalNet = fees.Sum(f => f.NetAmount),
                TotalPaid = fees.Where(f => f.Status == FeeStatus.Paid).Sum(f => f.NetAmount),
                TotalUnpaid = fees.Where(f => f.Status == FeeStatus.Unpaid).Sum(f => f.NetAmount),
                TotalOverdue = fees.Where(f => f.Status == FeeStatus.Overdue).Sum(f => f.NetAmount)
            };

            return Ok(report);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetStudentFees(int studentId)
        {
            var fees = await db.Fees
                .Where(f => f.StudentId == studentId)
                .Include(f => f.Course)
                .Include(f => f.Group)
                .ToListAsync();

            return Ok(fees);
        }

        [HttpGet("students/unpaid")]
        public async Task<IActionResult> GetUnpaidStudents()
        {
            var students = await db.Users
                .Include(s => s.Fees)
                .Include(s => s.Parent)
                .Where(s => s.Role == "Student")
                .ToListAsync();

            var unpaidStudents = students
                .Where(s => s.Fees.Any(f => f.Status != FeeStatus.Paid))
                .Select(s => new
                {
                    s.UserId,
                    s.Name,
                    s.PhoneNumber,
                    ParentPhone = s.Parent?.PhoneNumber,
                    UnpaidAmount = s.Fees
                        .Where(f => f.Status != FeeStatus.Paid)
                        .Sum(f => f.NetAmount)
                })
                .ToList();

            return Ok(unpaidStudents);
        }
    }
}