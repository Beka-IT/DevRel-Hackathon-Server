namespace WebApi.Models
{
    public class ProjectResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public long MaintainerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserResponseForProject> Members {  get; set; }
    }
}
