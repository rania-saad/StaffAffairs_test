using StaffAffairs.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StaffAffairs.Core.Interfaces
{
    public interface INationalityService
    {
        Task<Nationality> GetByIdAsync(int id);
        Task<IEnumerable<Nationality>> GetAllAsync();
        Task<Nationality> CreateAsync(Nationality nationality);
        Task UpdateAsync(Nationality nationality);
        Task DeleteAsync(int id);
        Task<Nationality> GetExactNationalityAsync(string name);
        Task<IEnumerable<Nationality>> SearchNationalitiesAsync(string name);
    }
}