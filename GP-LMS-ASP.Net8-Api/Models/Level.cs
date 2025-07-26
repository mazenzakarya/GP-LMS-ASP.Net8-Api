namespace GP_LMS_ASP.Net8_Api.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public string Name { get; set; }
        public int SessionsCount { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Groups> Groups { get; set; } = new HashSet<Groups>();
    }
}