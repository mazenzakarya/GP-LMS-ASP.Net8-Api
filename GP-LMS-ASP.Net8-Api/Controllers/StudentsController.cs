using GP_LMS_ASP.Net8_Api.Context;
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
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
    {
        var students = await _context.Users
            .Where(u => u.Role == "Student" && !u.IsDeleted)
            .Select(u => new StudentDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                DOB = u.DOB,
                Address = u.Address
            })
            .ToListAsync();

        return Ok(students);
    }

    // POST: api/students
    [HttpPost]
    public async Task<ActionResult> AddStudent(StudentDTO dto)
    {
        var student = new User
        {
            Name = dto.Name,
            Role = "Student",
            DOB = dto.DOB,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            IsDeleted = false,
            PasswordHash = "dummy", // Replace with proper password setup
            Username = GenerateUsername(dto.Name) // optional logic
        };

        _context.Users.Add(student);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Student added", student.UserId });
    }

    // GET: api/students/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
    {
        var student = await _context.Users
            .Where(u => u.Role == "Student" && u.UserId == id && !u.IsDeleted)
            .Select(u => new StudentDTO
            {
                UserId = u.UserId,
                Name = u.Name,
                Gender = u.Gender,
                PhoneNumber = u.PhoneNumber,
                DOB = u.DOB,
                Address = u.Address
            })
            .FirstOrDefaultAsync();

        if (student == null)
        {
            return NotFound(new { message = "Student not found." });
        }

        return Ok(student);
    }

    private string GenerateUsername(string name)
    {
        return name.ToLower().Replace(" ", "") + new Random().Next(100, 999);
    }
}