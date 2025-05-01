//using Microsoft.EntityFrameworkCore;
//using StaffAffairs.Core.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using StaffAffairs.EF;

//namespace StaffAffairs.Infrastructure.Data
//{
//    public class Repository<T> : IRepository<T> where T : class, IEntity
//    {
//        protected readonly StaffAffairsContext _context;
//        protected readonly DbSet<T> _dbSet;

//        public Repository(StaffAffairsContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<T>();
//        }

//        public async Task<T> GetByIdAsync(int id)
//        {
//            return await _dbSet.FindAsync(id);
//        }

//        public async Task<IEnumerable<T>> GetAllAsync()
//        {
//            return await _dbSet.ToListAsync();
//        }

//        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
//        {
//            return await _dbSet.Where(expression).ToListAsync();
//        }

//        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
//        {
//            return await _dbSet.AnyAsync(expression);
//        }

//        public async Task AddAsync(T entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }

//        public async Task AddRangeAsync(IEnumerable<T> entities)
//        {
//            await _dbSet.AddRangeAsync(entities);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(T entity)
//        {
//            _context.Entry(entity).State = EntityState.Modified;
//            await _context.SaveChangesAsync();
//        }

//        public async Task RemoveAsync(T entity)
//        {
//            _dbSet.Remove(entity);
//            await _context.SaveChangesAsync();
//        }

//        public async Task RemoveRangeAsync(IEnumerable<T> entities)
//        {
//            _dbSet.RemoveRange(entities);
//            await _context.SaveChangesAsync();
//        }




//}



//}

using Microsoft.EntityFrameworkCore;
using StaffAffairs.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StaffAffairs.EF;

namespace StaffAffairs.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly StaffAffairsContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(StaffAffairsContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
            => await _dbSet.Where(expression).ToListAsync();

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
            => await _dbSet.AnyAsync(expression);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        //public async Task<IEnumerable<T>> GetByNameAsync(string name, bool exactMatch = false)
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //        return await GetAllAsync();

        //    var parameter = Expression.Parameter(typeof(T), "e");
        //    var property = Expression.Property(parameter, "propertyName");
        //    var constant = Expression.Constant(name);

        //    Expression comparison = exactMatch
        //        ? Expression.Equal(property, constant)
        //        : Expression.Call(property,
        //            typeof(string).GetMethod("Contains", new[] { typeof(string) }),
        //            constant);

        //    var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
        //    return await _dbSet.Where(lambda).ToListAsync();
        //}

        //public async Task<IEnumerable<T>> GetByNameAsync(string name, bool exactMatch = false, string propertyName = "Name")
        //{
        //    if (string.IsNullOrWhiteSpace(name))
        //        return await GetAllAsync();

        //    var parameter = Expression.Parameter(typeof(T), "e");
        //    var property = typeof(T).GetProperty(propertyName) ??
        //                   throw new ArgumentException($"Property {propertyName} not found on type {typeof(T).Name}");

        //    var propertyAccess = Expression.Property(parameter, property);
        //    var constant = Expression.Constant(name);

        //    Expression comparison = exactMatch
        //        ? Expression.Equal(propertyAccess, constant)
        //        : Expression.Call(propertyAccess,
        //            typeof(string).GetMethod("Contains", new[] { typeof(string) })!,
        //            constant);

        //    var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
        //    return await _dbSet.Where(lambda).ToListAsync();
        //}


        public async Task<IEnumerable<T>> GetByNameAsync(string name, bool exactMatch = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await GetAllAsync();

            var parameter = Expression.Parameter(typeof(T), "e");

            // Try to find either NationalityName or SocialName property
            var property = typeof(T).GetProperty("NationalityName") ??
                          typeof(T).GetProperty("SocialName") ??
                          typeof(T).GetProperty("WorkStatusName") ?? 
                          typeof(T).GetProperty("MilitaryStateName") ??

                          throw new InvalidOperationException("Entity does not have a recognized name property");

            var propertyAccess = Expression.Property(parameter, property);
            var constant = Expression.Constant(name);

            Expression comparison;
            if (exactMatch)
            {
                comparison = Expression.Equal(propertyAccess, constant);
            }
            else
            {
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                comparison = Expression.Call(propertyAccess, containsMethod, constant);
            }

            var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

            return await _dbSet.Where(lambda).ToListAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
