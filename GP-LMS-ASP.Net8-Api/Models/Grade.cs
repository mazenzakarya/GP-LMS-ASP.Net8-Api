using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Grade
    {
        public int GradeId { get; set; }

        public int StudentId { get; set; }
        public virtual User Student { get; set; }

        public int SubjectId { get; set; }
        public virtual CourseSubject Subject { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("Element")]
        public int ElementId { get; set; }

        public virtual CourseSubjectElements Element { get; set; }

        public int Score { get; set; }
        public string? Notes { get; set; }
        public DateTime GradedAt { get; set; }
    }
}