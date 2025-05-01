using StaffAffairs.Core.Interfaces;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Services
{
    public class SocialService : ISocialService
    {
        private readonly IRepository<Social> _repository;

        public SocialService(IRepository<Social> repository)
        {
            _repository = repository;
        }

        public async Task<Social> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);


        public async Task<IEnumerable<Social>> GetAllAsync() => await _repository.GetAllAsync();


        public async Task<Social> CreateAsync(Social Social)
        {
            if (await _repository.ExistsAsync(n => n.Id == Social.Id))
                throw new ArgumentException("Social with this ID already exists");

            await _repository.AddAsync(Social);
            return Social;
        }


        public async Task UpdateAsync(Social Social)
        {
            var existing = await _repository.GetByIdAsync(Social.Id);
            if (existing == null)
                throw new KeyNotFoundException("Social not found");

            existing.SocialName = Social.SocialName;

            await _repository.UpdateAsync(existing); 
        }

        public async Task DeleteAsync(int id)
        {
            var Social = await _repository.GetByIdAsync(id);
            if (Social == null)
                throw new KeyNotFoundException("Social not found");

            await _repository.DeleteAsync(Social);
        }

        public async Task<Social> GetExactSocialAsync(string name)
        {
            var result = await _repository.GetByNameAsync(name, exactMatch: true);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Social>> SearchNationalitiesAsync(string name)
            => await _repository.GetByNameAsync(name);
    }
}