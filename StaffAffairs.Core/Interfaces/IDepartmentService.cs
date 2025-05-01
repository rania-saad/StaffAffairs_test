using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Models;

namespace StaffAffairs.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO> CreateAsync(DepartmentDTO Department);
        Task UpdateAsync(DepartmentDTO Department);
        Task DeleteAsync(int id);
        Task<DepartmentDTO> GetExactAsync(string name);
        Task<IEnumerable<DepartmentDTO>> SearchAsync(string name);
    }
}
