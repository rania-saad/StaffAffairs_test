using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class WorkStatusService : IWorkStatusService
    {
        private readonly IRepository<WorkStatus> _repository;

        public WorkStatusService(IRepository<WorkStatus> repository)
        {
            _repository = repository;
        }

        public async Task<WorkStatus> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<WorkStatus>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<WorkStatus> CreateAsync(WorkStatus WorkStatus)
        {
            if (await _repository.ExistsAsync(n => n.Id == WorkStatus.Id))
                throw new ArgumentException("WorkStatus with this ID already exists");

            await _repository.AddAsync(WorkStatus);
            return WorkStatus;
        }


        public async Task UpdateAsync(WorkStatus WorkStatus)
        {
            var existing = await _repository.GetByIdAsync(WorkStatus.Id);
            if (existing == null)
                throw new KeyNotFoundException("WorkStatus not found");

            // Update properties of the tracked entity
            existing.WorkStatusName = WorkStatus.WorkStatusName;
            // Update any other properties here...

            await _repository.UpdateAsync(existing); // Now working with tracked entity
        }

        public async Task DeleteAsync(int id)
        {
            var WorkStatus = await _repository.GetByIdAsync(id);
            if (WorkStatus == null)
                throw new KeyNotFoundException("WorkStatus not found");

            await _repository.DeleteAsync(WorkStatus);
        }

        public async Task<WorkStatus> GetExactWorkStatusAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name, exactMatch: true);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<WorkStatus>> SearchNationalitiesAsync(string name)
            => await _repository.GetByNameAsync(name);
    }
}