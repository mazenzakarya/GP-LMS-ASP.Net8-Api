using GP_LMS_ASP.Net8_Api.Context;
using GP_LMS_ASP.Net8_Api.DTOs;
using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly MyContext _context;

    public StudentsController(MyContext context)
    {
        _context = context;
    }

    // GET: api/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentithPerantDTO>>> GetStudents()
    {
        var students = await _context.Users
            .Where(u => u.Role == "Student" && !u.IsDeleted)
            .Include(u => u.Parent)
            .Select(u => new StudentithPerantDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Username = u.Username,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                DOB = u.DOB,
                Address = u.Address,
                Parent = u.Parent == null ? null : new ParentDTO
                {
                    ParentId = u.Parent.ParentId,
                    FatherName = u.Parent.FatherName,
                    MotherName = u.Parent.MotherName,
                    PhoneNumber = u.Parent.PhoneNumber
                }
            })
            .ToListAsync();

        return Ok(students);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult> AddStudent(StudentDTO dto)
    {
        // 1. Generate unique username automatically
        string baseUsername = dto.Name.Replace(" ", "").ToLower(); // remove spaces
        string generatedUsername = "";
        int counter = 1;

        do
        {
            generatedUsername = baseUsername + counter;
            counter++;
        } while (await _context.Users.AnyAsync(u => u.Username == generatedUsername));

        // 2. Create parent
        var parent = new Parent
        {
            FatherName = dto.FatherName,
            MotherName = dto.MotherName,
            PhoneNumber = dto.ParentPhoneNumber
        };
        _context.Parents.Add(parent);
        await _context.SaveChangesAsync();

        // 3. Create student
        var student = new User
        {
            Name = dto.Name,
            Role = "Student",
            DOB = dto.DOB,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            IsDeleted = false,
            Username = generatedUsername,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            ParentId = parent.ParentId
        };

        _context.Users.Add(student);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Student added successfully.",
            student.UserId,
            student.Username
        });
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
    {
        var student = await _context.Users
            .Where(u => u.Role == "Student" && u.UserId == id && !u.IsDeleted)
            .Include(u => u.Parent)
            .Select(u => new StudentDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                DOB = u.DOB,
                Address = u.Address,
                ParentId = u.ParentId,
                FatherName = u.Parent != null ? u.Parent.FatherName : null,
                MotherName = u.Parent != null ? u.Parent.MotherName : null,
                ParentPhoneNumber = u.Parent != null ? u.Parent.PhoneNumber : null
            })
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound(new { message = "Student not found." });
        }

        return Ok(student);
    }

    [HttpPut("move-student")]
    public async Task<IActionResult> MoveStudentToAnotherGroup([FromBody] MoveStudentGroupDTO dto)
    {
        // Check student exists
        var student = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == dto.StudentId && u.Role == "Student" && !u.IsDeleted);
        if (student == null)
            return NotFound($"Student with ID {dto.StudentId} not found.");

        // Check new group exists
        var newGroup = await _context.Groups.FindAsync(dto.NewGroupId);
        if (newGroup == null)
            return NotFound($"Group with ID {dto.NewGroupId} not found.");

        // Find current group assignment
        var studentGroup = await _context.StudentGroups
            .FirstOrDefaultAsync(sg => sg.StudentId == dto.StudentId);
        if (studentGroup == null)
            return NotFound("This student is not assigned to any group.");

        // Update the group
        studentGroup.GroupId = dto.NewGroupId;

        _context.StudentGroups.Update(studentGroup);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Student moved to new group successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteStudent(int id)
    {
        var student = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == id && u.Role == "Student");

        if (student == null)
            return NotFound($"Student with ID {id} not found.");

        if (student.IsDeleted)
            return BadRequest("Student is already deleted.");

        student.IsDeleted = true;
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Student with ID {id} has been soft deleted." });
    }

    private string GenerateUsername(string name)
    {
        return name.ToLower().Replace(" ", "") + new Random().Next(100, 999);
    }
}