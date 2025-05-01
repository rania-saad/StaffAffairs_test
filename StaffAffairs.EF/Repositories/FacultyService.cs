


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
    public class FacultyService : IFacultyService
    {
        private readonly IRepository<Faculty> _repository;
        private readonly IRepository<University> _Repository;
        private readonly IRepository<Department> _departmentService;


        private readonly StaffAffairsContext _dbContext;

        public FacultyService(IRepository<Faculty> repository, StaffAffairsContext dbContext, IRepository<University> Repository, IRepository<Department> departmentService)
        {
            _repository = repository;
            _dbContext = dbContext;
            _Repository = Repository;
            _departmentService = departmentService;
        }

        public async Task<FacultyDTO> GetByIdAsync(int id)
        {
            var Faculty = await _repository.GetByIdAsync(id);
            if (Faculty == null || Faculty.IsDeleted)
                return null;

            return new FacultyDTO
            {
                Id = Faculty.Id,
                FacultyName = Faculty.FacultyName,
                Priority = Faculty.Priority,
                UniversityId = Faculty.UniversityId,
            };
        }

        public async Task<FacultyDTO> CreateFacultyAsync(FacultyDTO facultyDto)
        {
            if (facultyDto == null)
                throw new ArgumentNullException(nameof(facultyDto));

            if (await _repository.ExistsAsync(f => f.Id == facultyDto.Id))
                throw new ArgumentException($"Faculty with ID {facultyDto.Id} already exists");

            var university = await _Repository.GetByIdAsync(facultyDto.UniversityId);
            if (university == null || university.IsDeleted)
                throw new KeyNotFoundException($"University with ID {facultyDto.UniversityId} not found or is deleted");

            var faculty = new Faculty
            {
                Id = facultyDto.Id,
                FacultyName = facultyDto.FacultyName,
                Priority = facultyDto.Priority,
                UniversityId = facultyDto.UniversityId,
                IsDeleted = false,
                Departments = new List<Department>()
            };

            await _repository.AddAsync(faculty);
            await _dbContext.SaveChangesAsync();

            return new FacultyDTO
            {
                Id = faculty.Id,
                FacultyName = faculty.FacultyName,
                Priority = faculty.Priority,
                UniversityId = faculty.UniversityId,
            };
        }

        public async Task<IEnumerable<FacultyDTO>> GetAllAsync()
        {
            var faculties = await _dbContext.Faculties
                .Where(f => !f.IsDeleted)
                .Include(f => f.University)
                .ToListAsync();

            return faculties.Select(f => new FacultyDTO
            {
                Id = f.Id,
                FacultyName = f.FacultyName,
                Priority = f.Priority,
                UniversityId = f.UniversityId,
            });
        }

        public async Task UpdateAsync(updateFacultyDTO faculty)
        {
            var existing = await _repository.GetByIdAsync(faculty.Id);
            if (existing == null || existing.IsDeleted)
                throw new KeyNotFoundException("Faculty not found");

            existing.FacultyName = faculty.FacultyName;

            var university = await _Repository.GetByIdAsync(existing.UniversityId);

            if (existing.UniversityId == null || university.IsDeleted)
            {
                throw new ArgumentException("Invalid University ID - University not found");

            }

            existing.Priority = faculty.Priority;
            existing.UniversityId = faculty.UniversityId;

            if (faculty.DepartmentIds != null && faculty.DepartmentIds.Count > 0)
            {
                var departments = await _dbContext.Departments
                    .Where(d => faculty.DepartmentIds.Contains(d.Id) && !d.IsDeleted)
                    .ToListAsync();

                existing.Departments.Clear();
                foreach (var dept in departments)
                {
                    existing.Departments.Add(dept);
                }
            }
            else
            {
                existing.Departments.Clear();
            }
            await _repository.UpdateAsync(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FacultyDTO> GetExactAsync(string name)
        {
            var Faculty  = await _dbContext.Faculties
                .FirstOrDefaultAsync(u => u.FacultyName == name && !u.IsDeleted);

            if (Faculty == null) return null;

            return new FacultyDTO 
            {
                Id = Faculty.Id,
                FacultyName = Faculty.FacultyName,
                Priority = Faculty.Priority,
                UniversityId=Faculty.UniversityId,

            };
        }


        public async Task<IEnumerable<FacultyDTO>> SearchAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<FacultyDTO>();

            var Faculties = await _dbContext.Faculties
                .Where(f => f.FacultyName.ToLower().Contains(name.Trim().ToLower())
                            && !f.IsDeleted)
                .ToListAsync();

            return Faculties.Select(f => new FacultyDTO
            {
                Id = f.Id,
                FacultyName = f.FacultyName,
                Priority = f.Priority,
                UniversityId = f.UniversityId,
            });
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


    }

}