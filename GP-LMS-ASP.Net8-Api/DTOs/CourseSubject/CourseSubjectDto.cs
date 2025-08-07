namespace GP_LMS_ASP.Net8_Api.DTOs.CourseSubject
{
    // CourseSubjectDto.cs
    public class CourseSubjectDto
    {
        public int CourseSubjectId { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    // CourseSubjectCreateDto.cs
    public class CourseSubjectCreateDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }

    // CourseSubjectUpdateDto.cs
    public class CourseSubjectUpdateDto
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}