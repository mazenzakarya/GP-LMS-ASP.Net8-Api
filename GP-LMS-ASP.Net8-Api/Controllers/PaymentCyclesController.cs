﻿using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentCyclesController : ControllerBase
    {
        private readonly MyContext db;
        private readonly IPaymentCycleService _service;

        public PaymentCyclesController(MyContext context, IPaymentCycleService service)
        {
            db = context;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentCycle(AddPaymentCycleDto dto)
        {
            var cycle = await _service.AddPaymentCycleAsync(dto);
            return Ok(cycle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cycle = await db.GroupPaymentCycles.FindAsync(id);

            if (cycle == null) return NotFound();

            cycle.IsDeleted = true;
            db.GroupPaymentCycles.Update(cycle);
            await db.SaveChangesAsync();

            return Ok(new { message = "Cycle soft deleted successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cycles = await db.GroupPaymentCycles

                .ToListAsync();

            return Ok(cycles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cycle = await db.GroupPaymentCycles
                .Where(c => c.GroupPaymentCycleId == id)
                .FirstOrDefaultAsync();
            if (cycle == null) return NotFound();
            return Ok(cycle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AddPaymentCycleDto dto)
        {
            var cycle = await db.GroupPaymentCycles.FindAsync(id);
            if (cycle == null) return NotFound();
            cycle.GroupId = dto.GroupId;
            cycle.StartDate = dto.StartDate;
            cycle.EndDate = dto.EndDate;
            cycle.MonthNumber = dto.MonthNumber;
            cycle.SessionsCount = dto.SessionsCount;
            db.GroupPaymentCycles.Update(cycle);
            await db.SaveChangesAsync();
            return Ok(cycle);
        }

        [HttpGet("by-group/{groupId}")]
        public async Task<IActionResult> GetByGroupId(int groupId)
        {
            var cycles = await db.GroupPaymentCycles
                .Where(c => c.GroupId == groupId)
                .ToListAsync();
            if (cycles == null || !cycles.Any()) return NotFound();
            return Ok(cycles);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPaymentCycles()
        {
            var cycles = await db.GroupPaymentCycles
                .Where(c => !c.IsDeleted)
                .Include(c => c.Group)
                .Select(c => new AddPaymentCycleDto
                {
                    GroupPaymentCycleId = c.GroupPaymentCycleId,
                    GroupId = c.GroupId,
                    GroupName = c.Group.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    MonthNumber = c.MonthNumber,
                    SessionsCount = c.SessionsCount,
                    PaymentStatus = c.PaymentStatus
                })
                .ToListAsync();

            return Ok(cycles);
        }
    }
}