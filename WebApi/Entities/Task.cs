using WebApi.Enums;

namespace WebApi.Entities
{
	public class Task : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public long ExecutorId { get; set; }
		public long AppointedBy { get; set; }
		public TaskDifficultyType Difficulty { get;set; }
		public TaskStatusType Status { get; set; } = TaskStatusType.UnderConsideration;
		public DateTime DeadLine { get; set; }
		public long ProjectId { get; set; }
		public Project Project { get; set; }
		public List<User> Users { get; } = new();
	}
}
