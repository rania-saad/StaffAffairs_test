
using Microsoft.EntityFrameworkCore;
using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using StaffAffairs.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> _repository;
        private readonly IRepository<JobType> _JobTypeRepo;


        private readonly StaffAffairsContext _dbContext;

        public JobService(IRepository<Job> repository, StaffAffairsContext dbContext, IRepository<JobType> JobTypeRepo)
        {
            _repository = repository;
            _dbContext = dbContext;
            _JobTypeRepo = JobTypeRepo;
        }

        public async Task<JobDTO> GetByIdAsync(int id)
        {
            var Job = await _repository.GetByIdAsync(id);
            if (Job == null )
                return null;

            return new JobDTO
            {
                Id = Job.Id,
                JobName = Job.JobName,
                JobPriority = Job.JobPriority,
                JobTypeId=Job.JobTypeId,
                EntedabTypeId = Job.EntedabTypeId,
            };
        }

        public async Task<JobDTO> CreateJobAsync(JobDTO JobDto)
        {
            if (JobDto == null)
                throw new ArgumentNullException(nameof(JobDto));

            if (await _repository.ExistsAsync(f => f.Id == JobDto.Id))
                throw new ArgumentException($"Job with ID {JobDto.Id} already exists");

            var JobType = await _JobTypeRepo.GetByIdAsync(JobDto.JobTypeId);
            if (JobType == null)
                throw new KeyNotFoundException($"University with ID {JobDto.JobTypeId} not found or is deleted");

            var Job = new Job
            {
                Id = JobDto.Id,
                JobName = JobDto.JobName,
                JobPriority = JobDto.JobPriority,
                JobTypeId = JobDto.JobTypeId,
                EntedabTypeId = JobDto.EntedabTypeId
            };

            await _repository.AddAsync(Job);
            await _dbContext.SaveChangesAsync();

            return new JobDTO
            {
                Id = Job.Id,
                JobName = Job.JobName,
                JobPriority = Job.JobPriority,
                JobTypeId = Job.JobTypeId,
                EntedabTypeId = Job.EntedabTypeId
            };
        }

        public async Task<IEnumerable<JobDTO>> GetAllAsync()
        {
            var jobs = await _dbContext.Jobs
                .Where(f => !false)
                .Include(f => f.JobType)
                .Include(f=>f.EntedabType)
                .ToListAsync();

            return jobs.Select(j => new JobDTO
            {
                Id = j.Id,
                JobName = j.JobName,
                JobPriority = j.JobPriority,
                JobTypeId = j.JobTypeId,
                EntedabTypeId = j.EntedabTypeId
            });
        }

        public async Task UpdateAsync(updateJobDTO Job)
        {
            var existing = await _repository.GetByIdAsync(Job.Id);
            if (existing == null )
                throw new KeyNotFoundException("Job not found");

            var JobType = await _JobTypeRepo.GetByIdAsync(existing.JobTypeId);

            if (existing.JobTypeId == null )
            {
                throw new ArgumentException("Invalid JobType ID - JobType not found");

            }

            if (Job.EntedabTypeId ==null )
            {
                throw new ArgumentException("Invalid EntedabType ID - EntedabType not found");

            }
           

            existing.JobName = Job.JobName;
            existing.JobPriority = Job.JobPriority;
            existing.EntedabTypeId = Job.EntedabTypeId;
            existing.JobTypeId= Job.JobTypeId;

            await _repository.UpdateAsync(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<JobDTO> GetExactAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Invalid job name  not found");

            }
            var Job = await _dbContext.Jobs
                .FirstOrDefaultAsync();
               
            if (Job == null) return null;

            return new JobDTO
            {
                Id = Job.Id,
                JobName = Job.JobName,
                JobPriority = Job.JobPriority,
                JobTypeId = Job.JobTypeId,
                EntedabTypeId = Job.EntedabTypeId

            };
        }


        public async Task<IEnumerable<JobDTO>> SearchAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<JobDTO>();

            var jobs = await _dbContext.Jobs
                .Where(f => f.JobName.ToLower().Contains(name.Trim().ToLower()) )
                .ToListAsync();

            return jobs.Select(j => new JobDTO
            {
                Id = j.Id,
                JobName = j.JobName,
                JobPriority = j.JobPriority,
                JobTypeId = j.JobTypeId,
                EntedabTypeId = j.EntedabTypeId
            });
        }

        public async Task DeleteAsync(int id)
        {
            var Job = await _repository.GetByIdAsync(id);
            if (Job != null)
            {
                await _repository.DeleteAsync(Job);
                await _dbContext.SaveChangesAsync();
            }
        }




    }

}