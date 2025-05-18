using Blog.DataAccess.Context;
using Blog.DataAccess.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BlogDbContext _dbContext;
        internal DbSet<T> dbSet;
        public Repository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<T>();
        }

       
        public virtual async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter, int pageSize = 100, int pageNumber = 1)
        {

            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, bool tracked = true,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var query = CreateQuery(filter, include);

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public IQueryable<T> CreateQuery(Expression<Func<T, bool>>? predicate = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = dbSet.AsNoTracking();

            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
    }
}
