using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrintManager.Logic.Models;
using PrintManager.Logic.Stores;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Repositories;

public class JobRepository(PrintingManagementContext context, IMapper mapper) : IJobStore
{
    public async Task<Job> CreateJobAsync(Job job)
    {
        JobEntity jobEntity = mapper.Map<JobEntity>(job);

        await context.Jobs.AddAsync(jobEntity);

        await context.SaveChangesAsync();

        return mapper.Map<Job>(jobEntity);
    }

    public Job CreateJob(Job job)
    {
        JobEntity jobEntity = mapper.Map<JobEntity>(job);

        context.Jobs.Add(jobEntity);

        context.SaveChanges();

        return mapper.Map<Job>(jobEntity);
    }

    public Job? GetById(int id)
    {
        JobEntity? jobEntity = context.Jobs
            .AsNoTracking()
            .FirstOrDefault(i => i.JobId == id);

        return mapper.Map<Job>(jobEntity);
    }

    public async Task<Job?> GetByIdAsync(int id)
    {
        JobEntity? jobEntity = await context.Jobs
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.JobId == id);

        return mapper.Map<Job>(jobEntity);
    }
}
