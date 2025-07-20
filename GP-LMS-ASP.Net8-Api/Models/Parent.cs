namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string FatherName { get; set; } = null!;
        public string MotherName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string NationalId { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
