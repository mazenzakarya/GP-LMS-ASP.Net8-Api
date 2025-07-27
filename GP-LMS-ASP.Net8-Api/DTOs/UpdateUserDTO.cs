namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class UpdateUserDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal? Salary { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public DateTime? DOB { get; set; }
        public string? Role { get; set; }
    }
}