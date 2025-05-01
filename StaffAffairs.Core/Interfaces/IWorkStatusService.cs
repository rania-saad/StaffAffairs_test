using StaffAffairs.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IWorkStatusService
    {
        Task<WorkStatus> GetByIdAsync(int id);
        Task<IEnumerable<WorkStatus>> GetAllAsync();
        Task<WorkStatus> CreateAsync(WorkStatus WorkStatus);
        Task UpdateAsync(WorkStatus WorkStatus);
        Task DeleteAsync(int id);
        Task<WorkStatus> GetExactWorkStatusAsync(string name);
        Task<IEnumerable<WorkStatus>> SearchNationalitiesAsync(string name);
    }
}