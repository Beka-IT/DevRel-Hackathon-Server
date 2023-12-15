using WebApi.Entities;

namespace WebApi.Models
{
    public class ProjectResponseForUsers
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public long MaintainerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
