using WebApi.Entities;

namespace WebApi.Models
{
    public class CommentResponse
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public long TaskId { get; set; }
        public long SenderId { get; set; }
        public UserResponse Sender { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
