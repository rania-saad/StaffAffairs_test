using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IUniversityService
    {
        Task<UniversityDTO> GetByIdAsync(int id);
        Task<IEnumerable<UniversityDTO>> GetAllAsync();
        Task<UniversityDTO> CreateAsync(University university);
        Task UpdateAsync(University University);
        Task DeleteAsync(int id);
        Task<UniversityDTO> GetExactUniversityAsync(string name);
        Task<IEnumerable<UniversityDTO>> SearchUniversityAsync(string name);

    }
}
