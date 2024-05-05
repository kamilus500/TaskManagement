using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Models.Dtos
{
    public class TaskJobDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string UserId { get; set; }
    }
}
