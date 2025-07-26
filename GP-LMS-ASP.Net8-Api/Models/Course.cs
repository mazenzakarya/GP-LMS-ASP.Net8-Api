namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Groups> Groups { get; set; } = new HashSet<Groups>();
        public ICollection<CourseSubject> Subjects { get; set; } = new HashSet<CourseSubject>();
    }
}