using System.ComponentModel.DataAnnotations.Schema;

namespace GP_LMS_ASP.Net8_Api.Models
{
    public class CourseSubjectElements
    {
        public int CourseSubjectElementsId { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        public virtual CourseSubject Subject { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}