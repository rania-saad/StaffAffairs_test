using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IJobTypeService
    {
        Task<JobTypeDTO> GetByIdAsync(int id);
        Task<IEnumerable<JobTypeDTO>> GetAllAsync();
        Task<JobTypeDTO> CreateAsync(JobTypeDTO JobType);
        //Task UpdateAsync(JobTypeDTO JobType);
        Task DeleteAsync(int id);
        Task<JobTypeDTO> GetExactAsync(string name);
        Task<IEnumerable<JobTypeDTO>> SearchAsync(string name);
    }
}
