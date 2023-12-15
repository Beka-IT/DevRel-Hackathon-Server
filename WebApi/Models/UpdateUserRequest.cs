namespace WebApi.Models
{
    public class UpdateUserRequest
    {
        public long Id { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
