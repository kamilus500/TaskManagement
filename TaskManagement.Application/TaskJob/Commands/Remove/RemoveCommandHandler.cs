using MediatR;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.TaskJob.Commands.Remove
{
    public class RemoveCommandHandler : IRequestHandler<RemoveCommand>
    {
        private readonly ITaskJobRepository _taskJobRepository;
        public RemoveCommandHandler(ITaskJobRepository taskJobRepository)
            => _taskJobRepository = taskJobRepository;

        public async Task Handle(RemoveCommand request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrEmpty(request.TaskJobId))
            {
                throw new ArgumentNullException(nameof(request.TaskJobId));
            }

            await _taskJobRepository.Delete(request.TaskJobId);
        }
    }
}
