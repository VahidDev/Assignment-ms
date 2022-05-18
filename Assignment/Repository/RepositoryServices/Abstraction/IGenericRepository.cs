using DomainModels.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAllAsync(IEnumerable<string> includingItems = null);
        Task<IEnumerable<T>> GetAllAsNoTrackingAsync(Expression<Func<T, bool>> predicate = null,
            IEnumerable<string> includingItems = null);
        Task<T> GetByIdAsync(int id, IEnumerable<string> includingItems = null);
        Task<T> GetByIdAsNoTrackingAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        bool Delete(T item);
        Task<bool> DeleteRangeAsync(IEnumerable<int>ids);
        bool Update(T entity);
        bool RemoveRangePermanently(ICollection<T> items);
        bool UpdateRange(IEnumerable<T> entity);
        Task<T> FirstOrDefaultAsync
            (Expression<Func<T, bool>> expression, IEnumerable<string> includingItems = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> FindAllAsync
            (Expression<Func<T, bool>> predicate, IEnumerable<string> includingItems = null);
        public Task<T> FirstOrDefaultAsNoTrackingAsync
          (Expression<Func<T, bool>> expression, IEnumerable<string> includingItems = null);
    }
}
