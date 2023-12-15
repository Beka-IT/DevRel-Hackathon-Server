using WebApi.Enums;

namespace WebApi.Models
{
    public class UserResponseForProject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronomyc { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActivated { get; set; }
        public RoleType Role { get; set; }
        public UserStatusType Status { get; set; }
        public long CompanyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
