namespace GP_LMS_ASP.Net8_Api.DTOs.Grade
{
    // GradeDto.cs
    public class GradeDto
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int ElementId { get; set; }
        public string ElementDescription { get; set; }
        public int Score { get; set; }
        public string? Notes { get; set; }
        public DateTime GradedAt { get; set; }
    }

    // GradeCreateDto.cs
    public class GradeCreateDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int ElementId { get; set; }
        public int Score { get; set; }
        public string? Notes { get; set; }
    }
}