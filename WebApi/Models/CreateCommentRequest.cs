namespace WebApi.Models
{
    public class CreateCommentRequest
    {
        public long SenderId { get; set; }
        public long TaskId { get; set; }
        public string Message { get; set; }
    }
}
