using Mapster;
using MediatR;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.TaskJob.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly ITaskJobRepository _taskJobRepository;
        public UpdateCommandHandler(ITaskJobRepository taskJobRepository)
            => _taskJobRepository = taskJobRepository;

        public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var updatedTaskJob = request.Adapt<Domain.Entities.TaskJob>();

            await _taskJobRepository.Update(updatedTaskJob, cancellationToken);
        }
    }
}
