using Blog.DataAccess.Context;
using Blog.DataAccess.Repository.Concrete;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BlogDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> keyValuePairs = new();
        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Repository<T> GetRepository<T>() where T : class
        {
            return (Repository<T>)keyValuePairs.GetOrAdd(key: typeof(T).Name, value: new Repository<T>(_dbContext));
        }
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
