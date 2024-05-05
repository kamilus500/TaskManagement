using Microsoft.EntityFrameworkCore;
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

        public async Task<string> Create(TaskJob job)
        {
            await _dbContext.TaskJobs.AddAsync(job);
            await _dbContext.SaveChangesAsync();

            return job.Id;
        }

        public async Task Delete(string jobId)
        {
            var taskJob = await _dbContext.TaskJobs.FirstOrDefaultAsync(x => x.Id == jobId);

            if (taskJob != null)
            {
                taskJob.IsDeleted = true;

                _dbContext.Update(taskJob);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskJob>> Get(string userId)
            => await _dbContext.TaskJobs.Where(x => x.UserId == userId).ToListAsync();

        public async Task<TaskJob> GetById(string jobId)
            => await _dbContext.TaskJobs.FirstOrDefaultAsync(x => x.Id == jobId);

        public async Task Update(TaskJob updatedJob)
        {
            _dbContext.Update(updatedJob);

            await _dbContext.SaveChangesAsync();
        }
    }
}
