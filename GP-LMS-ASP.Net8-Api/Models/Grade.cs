using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int SubjectId { get; set; }
        public CourseSubject Subject { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Element")]
        public int ElementId { get; set; }

        public CourseSubjectElements Element { get; set; }

        public int Score { get; set; }
        public string? Notes { get; set; }
        public DateTime GradedAt { get; set; }
    }
}