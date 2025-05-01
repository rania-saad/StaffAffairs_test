using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class NationalityService : INationalityService
    {
        private readonly IRepository<Nationality> _repository;

        public NationalityService(IRepository<Nationality> repository)
        {
            _repository = repository;
        }

        public async Task<Nationality> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Nationality>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Nationality> CreateAsync(Nationality nationality)
        {
            if (await _repository.ExistsAsync(n => n.Id == nationality.Id))
                throw new ArgumentException("Nationality with this ID already exists");

            await _repository.AddAsync(nationality);
            return nationality;
        }


        public async Task UpdateAsync(Nationality nationality)
        {
            var existing = await _repository.GetByIdAsync(nationality.Id);
            if (existing == null)
                throw new KeyNotFoundException("Nationality not found");

            // Update properties of the tracked entity
            existing.NationalityName = nationality.NationalityName;
            // Update any other properties here...

            await _repository.UpdateAsync(existing); // Now working with tracked entity
        }

        public async Task DeleteAsync(int id)
        {
            var nationality = await _repository.GetByIdAsync(id);
            if (nationality == null)
                throw new KeyNotFoundException("Nationality not found");

            await _repository.DeleteAsync(nationality);
        }

        public async Task<Nationality> GetExactNationalityAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name, exactMatch: true);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Nationality>> SearchNationalitiesAsync(string name)
            => await _repository.GetByNameAsync(name);
    }
}