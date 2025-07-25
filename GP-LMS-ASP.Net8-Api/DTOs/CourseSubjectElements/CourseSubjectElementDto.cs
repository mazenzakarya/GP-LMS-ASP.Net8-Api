namespace GP_LMS_ASP.Net8_Api.DTOs.CourseSubjectElements
{
    // CourseSubjectElementDto.cs
    public class CourseSubjectElementDto
    {
        public int CourseSubjectElementsId { get; set; }
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
    }

    // CourseSubjectElementCreateDto.cs
    public class CourseSubjectElementCreateDto
    {
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
    }

    // CourseSubjectElementUpdateDto.cs
    public class CourseSubjectElementUpdateDto
    {
        public int SubjectId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalMarks { get; set; }
    }

}
