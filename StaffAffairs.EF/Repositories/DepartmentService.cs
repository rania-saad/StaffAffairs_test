

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
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly StaffAffairsContext _dbContext;

        public DepartmentService(IRepository<Department> repository, StaffAffairsContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<DepartmentDTO> GetByIdAsync(int id)
        {
            var Department = await _repository.GetByIdAsync(id);
            if (Department == null || Department.IsDeleted)
                return null;

            return new DepartmentDTO
            {
                Id = Department.Id,
                DepartmentName = Department.DepartmentName,
                FacultyId= Department.FacultyId
            };
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var departments = await _dbContext.Departments
                .Where(d => !d.IsDeleted)
                .ToListAsync();

            return departments.Select(department => new DepartmentDTO
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                FacultyId = department.FacultyId
            });
        }

        public async Task<DepartmentDTO> CreateAsync(DepartmentDTO departmentDto)
        {
            if (await _repository.ExistsAsync(n => n.Id == departmentDto.Id))
                throw new ArgumentException("Department with this ID already exists");

            var department = new Department
            {
                Id = departmentDto.Id,
                DepartmentName = departmentDto.DepartmentName,
                FacultyId = departmentDto.FacultyId
            };

            await _repository.AddAsync(department);
            await _dbContext.SaveChangesAsync();

            return departmentDto;
        }

        public async Task UpdateAsync(DepartmentDTO Department)
        {
            var existing = await _repository.GetByIdAsync(Department.Id);
            if (existing == null || existing.IsDeleted)
                throw new KeyNotFoundException("Department not found");

            existing.DepartmentName = Department.DepartmentName;
            existing.FacultyId = Department.FacultyId;

            await _repository.UpdateAsync(existing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Department = await _repository.GetByIdAsync(id);
            if (Department != null)
            {
                Department.Delete();
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<DepartmentDTO> GetExactAsync(string name)
        {
            var Department = await _dbContext.Departments
                .FirstOrDefaultAsync(u => u.DepartmentName == name && !u.IsDeleted);

            if (Department == null) return null;

            return new DepartmentDTO
            {
                Id = Department.Id,
                DepartmentName = Department.DepartmentName,
                FacultyId = Department.FacultyId

            };
        }

      
        public async Task<IEnumerable<DepartmentDTO>> SearchAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<DepartmentDTO>();

            var departments = await _dbContext.Departments
                .Where(d => d.DepartmentName.ToLower().Contains(name.Trim().ToLower())
                            && !d.IsDeleted)
                .ToListAsync();

            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                DepartmentName = d.DepartmentName,
                FacultyId = d.FacultyId
            });
        }

    }

    
}