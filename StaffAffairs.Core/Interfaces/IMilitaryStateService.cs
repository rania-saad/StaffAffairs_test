using StaffAffairs.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface IMilitaryStateService
    {
        Task<MilitaryState> GetByIdAsync(int id);
        Task<IEnumerable<MilitaryState>> GetAllAsync();
        Task<MilitaryState> CreateAsync(MilitaryState MilitaryState);
        Task UpdateAsync(MilitaryState MilitaryState);
        Task DeleteAsync(int id);
        Task<MilitaryState> GetExactMilitaryStateAsync(string name);
        Task<IEnumerable<MilitaryState>> SearchMilitaryStateAsync(string name);
    }
}