using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Persistance;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskJobRepository : ITaskJobRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TaskJobRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<string> Create(TaskJob job, CancellationToken cancellationToken)
        {
            await _dbContext.TaskJobs.AddAsync(job);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return job.Id;
        }

        public async Task Delete(string jobId, CancellationToken cancellationToken)
        {
            var taskJob = await _dbContext.TaskJobs.FirstOrDefaultAsync(x => x.Id == jobId);

            if (taskJob != null)
            {
                taskJob.IsDeleted = true;

                _dbContext.Update(taskJob);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<TaskJob>> Get(string userId, CancellationToken cancellationToken)
            => await _dbContext.TaskJobs.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        public async Task<TaskJob> GetById(string jobId, CancellationToken cancellationToken)
            => await _dbContext.TaskJobs.FirstOrDefaultAsync(x => x.Id == jobId, cancellationToken);

        public async Task Update(TaskJob updatedJob, CancellationToken cancellationToken)
        {
            _dbContext.Update(updatedJob);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
