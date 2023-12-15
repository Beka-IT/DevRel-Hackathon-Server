using WebApi.Enums;

namespace WebApi.Models
{
    public class ChangeTaskStatusRequest
    {
        public long Id { get; set; }
        public TaskStatusType Status { get; set; }
        public long? ExecutorId { get; set; }

    }
}
