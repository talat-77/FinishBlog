using Blog.DataAccess.Abstraction;
using Blog.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly BlogDbContext _context;

        public GenericRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
      => await _context.Set<T>().CountAsync();

        public async Task CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var deleted = await _context.Set<T>().FindAsync(id);
            _context.Remove(deleted);
            await _context.SaveChangesAsync();

        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> GetListAsnc()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
