using System.Reflection.Metadata;
using WebApi.Enums;

namespace WebApi.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string? Patronomyc { get; set; }
		public string Position { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public bool IsActivated { get; set; }
		public RoleType Role { get; set; }
		public long CompanyId { get; set; }
		public Company Company { get; set; }
		public List<Task> Tasks { get; } = new();
		public List<Project> Projects { get; } = new();
	}
}
