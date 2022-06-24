using DomainModels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using Repository.Utilities.GenericRepositoryUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly AppDbContext context;
        protected DbSet<T> dbSet;
        protected IQueryable<T> _querable;
        protected readonly ILogger _logger;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;
            dbSet = context.Set<T>();
            _querable = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync
            (IEnumerable<string> includingItems = null)
        {
            _querable=_querable.IncludeItemsIfExist(includingItems);
            return await _querable.Where(t => !t.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsNoTrackingAsync
            (Expression<Func<T, bool>> predicate, 
            IEnumerable<string> includingItems = null)
        {
            _querable = _querable.IncludeItemsIfExist(includingItems);
            return await _querable.Where(predicate).AsNoTracking().ToListAsync();
        }
        public virtual async Task<T> GetByIdAsync
            (int id, IEnumerable<string> includingItems = null)
        {
            _querable= _querable.IncludeItemsIfExist(includingItems);
            return await _querable.FirstOrDefaultAsync(t=>t.Id==id&&!t.IsDeleted);
        }
        public virtual async Task<T> GetByIdAsNoTrackingAsync(int id)
        {
            return await dbSet.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }
        public virtual async Task<bool> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }
        public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return true;
        }
        public virtual bool Delete(T item)
        {
            item.IsDeleted = true;
            dbSet.Update(item);
            return true;
        }
        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<int> ids)
        {
            ICollection<T>items=new List<T>();
            foreach (int id in ids)
            {
                T item = await dbSet
                    .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
                if(item==null)return false;
                item.IsDeleted = true;
                items.Add(item);
            }
            dbSet.UpdateRange(items);
            return true;
        }

        public virtual bool Update(T entity)
        {
            dbSet.Update(entity);
            return true;
        }
        public virtual bool UpdateRange(IEnumerable<T> entities)
        {
            dbSet.UpdateRange(entities);
            return true;
        }
        public async Task<T> FirstOrDefaultAsync
            (Expression<Func<T, bool>> expression, IEnumerable<string> includingItems = null)
        {
            _querable = _querable.IncludeItemsIfExist(includingItems);
            return await _querable.FirstOrDefaultAsync(expression);
        }
        public async Task<T> FirstOrDefaultAsNoTrackingAsync
           (Expression<Func<T, bool>> expression, IEnumerable<string> includingItems = null)
        {
            _querable = _querable.IncludeItemsIfExist(includingItems);
            return await _querable.AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
                return await dbSet.AnyAsync(expression);

            return await dbSet.AnyAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync
            (Expression<Func<T, bool>> predicate, IEnumerable<string> includingItems = null)
        {
            _querable = _querable.IncludeItemsIfExist(includingItems);
            return await _querable.Where(predicate).ToListAsync();
        }

        public bool RemoveRangePermanently(ICollection<T>items)
        {
            context.ChangeTracker.Clear();
            dbSet.RemoveRange(items);
            return true;
        }
    }
}
