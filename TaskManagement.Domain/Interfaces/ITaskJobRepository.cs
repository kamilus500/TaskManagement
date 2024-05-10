using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskJobRepository
    {
        Task<IEnumerable<TaskJob>> Get(string userId, CancellationToken cancellationToken);

        Task<TaskJob> GetById(string jobId, CancellationToken cancellationToken);

        Task<string> Create(TaskJob job, CancellationToken cancellationToken);

        Task Delete(string jobId, CancellationToken cancellationToken);

        Task Update(TaskJob updatedJob, CancellationToken cancellationToken);
    }
}
