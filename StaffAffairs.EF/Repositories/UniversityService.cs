

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
    public class UniversityService : IUniversityService
    {
        private readonly IRepository<University> _repository;
        private readonly StaffAffairsContext _dbContext;

        public UniversityService(IRepository<University> repository, StaffAffairsContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<UniversityDTO> GetByIdAsync(int id)
        {
            var university = await _repository.GetByIdAsync(id);
            if (university == null || university.IsDeleted)
                return null;

            return new UniversityDTO
            {
                Id = university.Id,
                UniversityName = university.UniversityName,
                Foreign_University = university.Foreign_University
            };
        }

        public async Task<IEnumerable<UniversityDTO>> GetAllAsync()
        {
            return await _dbContext.Universities
                .Where(u => !u.IsDeleted)
                .Select(u => new UniversityDTO
                {
                    Id = u.Id,
                    UniversityName = u.UniversityName,
                    Foreign_University = u.Foreign_University
                })
                .ToListAsync();
        }

        public async Task<UniversityDTO> CreateAsync(University university)
        {
            if (await _repository.ExistsAsync(n => n.Id == university.Id))
                throw new ArgumentException("University with this ID already exists");

            await _repository.AddAsync(university);
            await _dbContext.SaveChangesAsync();

            return new UniversityDTO
            {
                Id = university.Id,
                UniversityName = university.UniversityName,
                Foreign_University = university.Foreign_University
            };
        }

        public async Task UpdateAsync(University university)
        {
            var existing = await _repository.GetByIdAsync(university.Id);
            if (existing == null || existing.IsDeleted)
                throw new KeyNotFoundException("University not found");

            existing.UniversityName = university.UniversityName;
            existing.Foreign_University = university.Foreign_University;

            await _repository.UpdateAsync(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var university = await _repository.GetByIdAsync(id);
            if (university != null)
            {
                university.Delete();
                await _dbContext.SaveChangesAsync();
            }
        }

  
        public async Task<UniversityDTO> GetExactUniversityAsync(string name)
        {
            var university = await _dbContext.Universities
                .FirstOrDefaultAsync(u => u.UniversityName == name && !u.IsDeleted);

            if (university == null) return null;

            return new UniversityDTO
            {
                Id = university.Id,
                UniversityName = university.UniversityName,
                Foreign_University = university.Foreign_University
            };
        }

        public async Task<IEnumerable<UniversityDTO>> SearchUniversityAsync(string name)
        {
            return (await _repository.GetByNameAsync(name))
                .Where(u => !u.IsDeleted)
                .Select(u => new UniversityDTO
                {
                    Id = u.Id,
                    UniversityName = u.UniversityName,
                    Foreign_University = u.Foreign_University
                });
        }
    }
}