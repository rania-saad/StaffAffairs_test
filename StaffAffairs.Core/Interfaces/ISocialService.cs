using StaffAffairs.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface ISocialService
    {
        Task<Social> GetByIdAsync(int id);
        Task<IEnumerable<Social>> GetAllAsync();
        Task<Social> CreateAsync(Social Social);
        Task UpdateAsync(Social Social);
        Task DeleteAsync(int id);
        Task<Social> GetExactSocialAsync(string name);
        Task<IEnumerable<Social>> SearchNationalitiesAsync(string name);
    }
}