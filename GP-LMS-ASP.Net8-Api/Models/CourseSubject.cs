using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class CourseSubject
    {
        public int CourseSubjectId { get; set; }
        public string Name { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<CourseSubjectElements> Elements { get; set; } = new HashSet<CourseSubjectElements>();
    }
}
