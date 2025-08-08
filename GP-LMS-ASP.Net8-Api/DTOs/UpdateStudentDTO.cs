namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class UpdateStudentDTO
    {
        public string? Name { get; set; }
        public string PasswordHash { get; set; } // Assuming this is a hash of the password
    }
}