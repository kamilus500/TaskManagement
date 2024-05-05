using MediatR;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Queries.Get
{
    public class GetQuery: IRequest<IEnumerable<TaskJobDto>>
    {
        public string UserId { get; set; }
        public GetQuery(string userId)
        {
            UserId = userId;
        }
    }
}
