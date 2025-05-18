using Blog.DataAccess.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
