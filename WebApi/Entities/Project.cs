namespace WebApi.Entities
{
	public class Project : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public long CompanyId { get; set; }
		public Company Company { get; set; }
		public ICollection<Task> Tasks { get; }
		public List<User> Employees { get; set; }
	}
}
