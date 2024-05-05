using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Queries.GetById
{
    public class GetByIdQuery : IRequest<TaskJobDto>
    {
        public string TaskJobId { get; set; }
        public GetByIdQuery(string taskJobId)
        {
            TaskJobId = taskJobId;
        }
    }
}
