namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } = "Student";
    }
}