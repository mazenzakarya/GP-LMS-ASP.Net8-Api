namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Groups
    {
        public int GroupsId { get; set; }
        public string Name { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int TeacherId { get; set; }
        public virtual User Teacher { get; set; }

        public decimal Amount { get; set; }
        public DateTime LevelStartDate { get; set; }
        public DateTime LevelEndDate { get; set; }
        public DateTime NextPaymentDate { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<StudentGroup> StudentGroups { get; set; } = new HashSet<StudentGroup>();
        public ICollection<CourseSubjectElements> CourseSubjectElements { get; set; } = new HashSet<CourseSubjectElements>();
    }
}