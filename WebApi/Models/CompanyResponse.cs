using WebApi.Entities;

namespace WebApi.Models
{
    public class CompanyResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string BusinessDescription { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public long? OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserResponse> Employees { get; set; }
        public List<ProjectResponse> Projects { get; set; }
    }
}
