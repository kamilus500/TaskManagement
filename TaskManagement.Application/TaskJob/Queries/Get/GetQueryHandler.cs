using Mapster;
using MediatR;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Queries.Get
{
    public class GetQueryHandler: IRequestHandler<GetQuery, IEnumerable<TaskJobDto>>
    {
        private readonly ITaskJobRepository _taskJobRepository;
        public GetQueryHandler(ITaskJobRepository taskJobRepository)
            => _taskJobRepository = taskJobRepository;

        public async Task<IEnumerable<TaskJobDto>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(request.UserId))
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }

            var taskJobs = await _taskJobRepository.Get(request.UserId);

            return taskJobs.Adapt<IEnumerable<TaskJobDto>>();
        }
    }
}
