namespace GP_LMS_ASP.Net8_Api.DTOs
{
    public class GroupDTO
    {
        public int GroupsId { get; set; }
        public string Name { get; set; }

        public int CourseId { get; set; }
        public string? CourseName { get; set; } // still optional
        public string LevelDescription { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public decimal Amount { get; set; }
        public DateTime LevelStartDate { get; set; }
        public DateTime LevelEndDate { get; set; }
        public DateTime NextPaymentDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}