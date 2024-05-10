using Mapster;
using MediatR;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Domain.Models.Dtos;

namespace TaskManagement.Application.TaskJob.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery,TaskJobDto>
    {
        private readonly ITaskJobRepository _taskJobRepository;
        public GetByIdQueryHandler(ITaskJobRepository taskJobRepository)
            => _taskJobRepository = taskJobRepository;

        public async Task<TaskJobDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.TaskJobId))
            {
                throw new ArgumentNullException(nameof(request.TaskJobId));
            }

            var taskJobs = await _taskJobRepository.GetById(request.TaskJobId, cancellationToken);

            return taskJobs.Adapt<TaskJobDto>();
        }
    }
}
