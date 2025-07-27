namespace GP_LMS_ASP.Net8_Api.DTOs.AssignGroupTeacher
{
    public class TeacherWithGroupsDto
    {

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public List<string> GroupNames { get; set; }
    }
}
