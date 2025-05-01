
using Microsoft.EntityFrameworkCore;
using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using StaffAffairs.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IRepository<JobType> _repository;
        private readonly StaffAffairsContext _dbContext;

        public JobTypeService(IRepository<JobType> repository, StaffAffairsContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<JobTypeDTO> GetByIdAsync(int id)
        {
            var JobType = await _repository.GetByIdAsync(id);
            if (JobType == null)
                return null;

            return new JobTypeDTO
            {
                Id = JobType.Id,
                JobTypeName = JobType.JobTypeName,
            };
        }

        public async Task<IEnumerable<JobTypeDTO>> GetAllAsync()
        {
            return await _dbContext.jobTypes
                .Select(J => new JobTypeDTO
                {
                    Id = J.Id,
                    JobTypeName = J.JobTypeName,
                })
                .ToListAsync();
        }

        public async Task<JobTypeDTO> CreateAsync(JobTypeDTO JobTypeDto)
        {
            if (await _repository.ExistsAsync(n => n.Id == JobTypeDto.Id))
                throw new ArgumentException("JobType with this ID already exists");

            var jobType = new JobType
            {
                Id = JobTypeDto.Id,
                JobTypeName = JobTypeDto.JobTypeName,
            };

            await _repository.AddAsync(jobType);
            await _dbContext.SaveChangesAsync();

            return JobTypeDto;
        }
        public async Task UpdateAsync(JobTypeDTO JobType)
        {
            var existing = await _repository.GetByIdAsync(JobType.Id);
            if (existing == null)
                throw new KeyNotFoundException("JobType not found");

            existing.JobTypeName = JobType.JobTypeName;

            await _repository.UpdateAsync(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var JobType = await _repository.GetByIdAsync(id);
            if (JobType != null)
            {
                await _repository.DeleteAsync(JobType);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<JobTypeDTO> GetExactAsync(string name)
        {
            var JobType = await _dbContext.jobTypes
                .FirstOrDefaultAsync(u => u.JobTypeName == name );

            if (JobType == null) return null;

            return new JobTypeDTO
            {
                Id = JobType.Id,
                JobTypeName = JobType.JobTypeName,
            };
        }

        public async Task<IEnumerable<JobTypeDTO>> SearchAsync(string name)
        {
            return (await _repository.GetAllAsync())
                .Where(x => x.JobTypeName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Select(u => new JobTypeDTO
                {
                    Id = u.Id,
                    JobTypeName = u.JobTypeName,
                });
        }
       
    }
}