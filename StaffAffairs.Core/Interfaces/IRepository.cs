// IRepository.cs in StaffAffairs.Core
using StaffAffairs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace StaffAffairs.Core.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
       Task DeleteAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
         Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetByNameAsync(string name, bool exactMatch = false);


    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace StaffAffairs.Core.Interfaces
//{
//    public interface IRepository<T> where T : class, IEntity
//    {
//        Task<T> GetByIdAsync(int id);
//        Task<IEnumerable<T>> GetAllAsync();
//        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
//        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
//        Task AddAsync(T entity);
//        Task AddRangeAsync(IEnumerable<T> entities);
//        Task UpdateAsync(T entity);
//        Task DeleteAsync(T entity);
//        Task DeleteRangeAsync(IEnumerable<T> entities);
//        Task<IEnumerable<T>> GetByNameAsync(string name, bool exactMatch = false);
//    }
//}