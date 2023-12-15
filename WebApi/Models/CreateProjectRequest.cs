using WebApi.Entities;

namespace WebApi.Models
{
    public class CreateProjectRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public long MaintainerId { get; set; }
    }
}
