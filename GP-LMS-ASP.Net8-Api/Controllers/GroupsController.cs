using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly MyContext _context;

        public GroupsController(MyContext context)
        {
            _context = context;
        }

        // GET: api/groups 
        //Get all groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDTO>>> GetAllGroups()
        {
            var groups = await _context.Groups
                .Where(g => !g.IsDeleted)
                .Include(g => g.Course)
                .Include(g => g.Level)
                .Include(g => g.Teacher)
                .Select(g => new GroupDTO
                {
                    GroupsId = g.GroupsId,
                    Name = g.Name,
                    CourseId = g.CourseId,
                    CourseName = g.Course.Name,
                    LevelId = g.LevelId,
                    LevelName = g.Level.Name,
                    TeacherId = g.TeacherId,
                    TeacherName = g.Teacher.Name,
                    Amount = g.Amount,
                    LevelStartDate = g.LevelStartDate,
                    LevelEndDate = g.LevelEndDate,
                    NextPaymentDate = g.NextPaymentDate,
                    IsDeleted = g.IsDeleted
                })
                .ToListAsync();

            return Ok(groups);
        }

        // GET: api/groups/5
        //Get group by id
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDTO>> GetGroupById(int id)
        {
            var group = await _context.Groups
                .Where(g => g.GroupsId == id && !g.IsDeleted)
                .Include(g => g.Course)
                .Include(g => g.Level)
                .Include(g => g.Teacher)
                .Select(g => new GroupDTO
                {
                    GroupsId = g.GroupsId,
                    Name = g.Name,
                    CourseId = g.CourseId,
                    CourseName = g.Course.Name,
                    LevelId = g.LevelId,
                    LevelName = g.Level.Name,
                    TeacherId = g.TeacherId,
                    TeacherName = g.Teacher.Name,
                    Amount = g.Amount,
                    LevelStartDate = g.LevelStartDate,
                    LevelEndDate = g.LevelEndDate,
                    NextPaymentDate = g.NextPaymentDate,
                    IsDeleted = g.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (group == null)
                return NotFound(new { message = "Group not found." });

            return Ok(group);
        }

        // POST: api/groups
        //Add group
        [HttpPost]
        public async Task<ActionResult> CreateGroup(GroupDTO dto)
        {
            var group = new Groups
            {
                Name = dto.Name,
                CourseId = dto.CourseId,
                LevelId = dto.LevelId,
                TeacherId = dto.TeacherId,
                Amount = dto.Amount,
                LevelStartDate = dto.LevelStartDate,
                LevelEndDate = dto.LevelEndDate,
                NextPaymentDate = dto.NextPaymentDate,
                IsDeleted = false
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Group created successfully", groupId = group.GroupsId });
        }

        // PUT: api/groups/5
        //Update group
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroup(int id, GroupDTO dto)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null || group.IsDeleted)
                return NotFound(new { message = "Group not found." });

            group.Name = dto.Name;
            group.CourseId = dto.CourseId;
            group.LevelId = dto.LevelId;
            group.TeacherId = dto.TeacherId;
            group.Amount = dto.Amount;
            group.LevelStartDate = dto.LevelStartDate;
            group.LevelEndDate = dto.LevelEndDate;
            group.NextPaymentDate = dto.NextPaymentDate;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Group updated successfully" });
        }

        // DELETE: api/groups/5
        //Soft delte a group
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null || group.IsDeleted)
                return NotFound(new { message = "Group not found." });

            group.IsDeleted = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Group deleted (soft delete)." });
        }
    }
}
