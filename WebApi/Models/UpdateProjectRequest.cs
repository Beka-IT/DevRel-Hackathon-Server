namespace WebApi.Models
{
    public class UpdateProjectRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long MaintainerId { get; set; }
    }
}
