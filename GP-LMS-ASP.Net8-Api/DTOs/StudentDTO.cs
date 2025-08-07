public class StudentDTO
{
    public int UserId { get; set; }
    public  string Name { get; set; }
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }


    public string Username { get; set; }
    public string Password { get; set; }


    public DateTime? DOB { get; set; }
    public string? Address { get; set; }

    public int? ParentId { get; set; }
    public string? FatherName { get; set; }
    public string? MotherName { get; set; }
    public string? ParentPhoneNumber { get; set; }
}