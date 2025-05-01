using StaffAffairs.Core.DTOs;
using StaffAffairs.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IJobService
    {

        Task<JobDTO> GetByIdAsync(int id);
        Task<IEnumerable<JobDTO>> GetAllAsync();

        Task<JobDTO> CreateJobAsync(JobDTO JobDto);

        Task UpdateAsync(updateJobDTO Job);

        Task<JobDTO> GetExactAsync(string name);
        Task<IEnumerable<JobDTO>> SearchAsync(string name);
        Task DeleteAsync(int id);

    }
}
