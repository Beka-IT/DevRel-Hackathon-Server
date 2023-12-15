using WebApi.Enums;

namespace WebApi.Models
{
    public class UpdateTaskRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long? ExecutorId { get; set; }
        public TaskDifficultyType Difficulty { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
