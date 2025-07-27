namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class GroupStudentDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }

        public string PaymentStatus { get; set; }

        // Parent Info
        public int? ParentId { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? ParentPhoneNumber { get; set; }
    }

}