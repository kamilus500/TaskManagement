using Mapster;
using MediatR;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.TaskJob.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, string>
    {
        private readonly ITaskJobRepository _taskJobRepository;
        public CreateCommandHandler(ITaskJobRepository taskJobRepository)
            => _taskJobRepository = taskJobRepository;

        public async Task<string> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var taskJob = request.Adapt<Domain.Entities.TaskJob>();

            taskJob.Id = Guid.NewGuid().ToString();
            taskJob.CreationDate = DateTime.Now;
            taskJob.Status = Domain.Enums.StatusEnum.New;
            taskJob.IsDeleted = false;
            
            var taskJobId = await _taskJobRepository.Create(taskJob);

            return taskJobId;
        }
    }
}
