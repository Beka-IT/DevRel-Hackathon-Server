using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
	public class Company : BaseEntity
	{
		public string Name { get; set; }
		public string BusinessDescription { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public long? OwnerId { get; set; }
		public ICollection<User> Employees { get; }
		public ICollection<Project> Projects { get; }
	}
}
