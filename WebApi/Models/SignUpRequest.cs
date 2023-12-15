using WebApi.Entities;
using WebApi.Enums;

namespace WebApi.Models
{
	public class SignUpRequest
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
		public long CompanyId { get; set; }
	}
}
