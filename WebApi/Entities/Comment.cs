namespace WebApi.Entities
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }
        public Task Task { get; set; }
        public long TaskId { get; set; }
        public long SenderId { get; set; }
    }
}
