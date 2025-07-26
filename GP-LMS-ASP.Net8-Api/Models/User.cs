namespace GP_LMS_ASP.Net8_Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public DateTime? DOB { get; set; }
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }
        public string? Image { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public decimal? Salary { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<StudentGroup> StudentGroups { get; set; } = new HashSet<StudentGroup>();
        public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();
        public virtual ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
        public virtual ICollection<Fee> Fees { get; set; } = new HashSet<Fee>();
    }
}