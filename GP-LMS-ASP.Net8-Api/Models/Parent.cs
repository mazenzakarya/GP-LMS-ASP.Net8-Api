namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Parent
    {
        public int ParentId { get; set; }
        public string FatherName { get; set; }
        public string? MotherName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? NationalId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}