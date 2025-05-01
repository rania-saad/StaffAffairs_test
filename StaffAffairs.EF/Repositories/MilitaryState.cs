using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class MilitaryStateService : IMilitaryStateService
    {
        private readonly IRepository<MilitaryState> _repository;

        public MilitaryStateService(IRepository<MilitaryState> repository)
        {
            _repository = repository;
        }

        public async Task<MilitaryState> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<MilitaryState>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<MilitaryState> CreateAsync(MilitaryState MilitaryState)
        {
            if (await _repository.ExistsAsync(n => n.Id == MilitaryState.Id))
                throw new ArgumentException("MilitaryState with this ID already exists");

            await _repository.AddAsync(MilitaryState);
            return MilitaryState;
        }


        public async Task UpdateAsync(MilitaryState MilitaryState)
        {
            var existing = await _repository.GetByIdAsync(MilitaryState.Id);
            if (existing == null)
                throw new KeyNotFoundException("MilitaryState not found");

            existing.MilitaryStateName = MilitaryState.MilitaryStateName;

            await _repository.UpdateAsync(existing); 
        }

        public async Task DeleteAsync(int id)
        {
            var MilitaryState = await _repository.GetByIdAsync(id);
            if (MilitaryState == null)
                throw new KeyNotFoundException("MilitaryState not found");

            await _repository.DeleteAsync(MilitaryState);
        }

        public async Task<MilitaryState> GetExactMilitaryStateAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name, exactMatch: true);
            return result.FirstOrDefault();
        }

        //public async Task<IEnumerable<MilitaryState>> SearchNationalitiesAsync(string name)
        //    => await _repository.GetByNameAsync(name);

        public async Task<IEnumerable<MilitaryState>> SearchMilitaryStateAsync(string name)
    => await _repository.GetByNameAsync(name);
    }
}