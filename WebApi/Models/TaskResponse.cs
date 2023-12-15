using WebApi.Entities;
using WebApi.Enums;

namespace WebApi.Models
{
    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? ExecutorId { get; set; }
        public long AppointedBy { get; set; }
        public TaskDifficultyType Difficulty { get; set; }
        public TaskStatusType Status { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime CreatedAt {  get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}
