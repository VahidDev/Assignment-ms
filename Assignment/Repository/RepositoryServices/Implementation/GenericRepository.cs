using DomainModels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
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

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.Where(t => !t.IsDeleted).ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
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
        public virtual async Task<bool> DeleteAsync(int id)
        {
            T item = await dbSet
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            item.IsDeleted = true;
            item.DeletedAt = DateTime.UtcNow;
            return true;
        }

        public virtual bool Update(T entity)
        {
            dbSet.Update(entity);
            return true;
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
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
            if (includingItems != null)
            {
                foreach (string item in includingItems)
                {
                    _querable.Include(item);
                }
            }
            return await _querable.Where(predicate).ToListAsync();
        }

    }
}
