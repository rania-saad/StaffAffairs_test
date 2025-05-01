using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IFacultyService
    {

        Task<FacultyDTO> GetByIdAsync(int id);
        Task<IEnumerable<FacultyDTO>> GetAllAsync();

        Task<FacultyDTO> CreateFacultyAsync(FacultyDTO facultyDto);

        Task UpdateAsync(updateFacultyDTO faculty);        

        Task<FacultyDTO> GetExactAsync(string name);
        Task<IEnumerable<FacultyDTO>> SearchAsync(string name);

        Task DeleteAsync(int id);

    }
}
