using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    public interface ITaskJobRepository
    {
        Task<IEnumerable<TaskJob>> Get(string userId);

        Task<TaskJob> GetById(string jobId);

        Task<string> Create(TaskJob job);

        Task Delete(string jobId);

        Task Update(TaskJob updatedJob);
    }
}
