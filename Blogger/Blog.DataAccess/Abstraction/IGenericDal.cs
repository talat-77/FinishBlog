using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blog.DataAccess.Abstraction
{
    public interface IGenericDal<T> where T : class
    {
        Task CreateAsync(T entity);
        Task Delete(Guid id);
        Task UpdateAsync(T entity);
        Task<List<T>> GetListAsnc();
        Task<T> GetByIdAsync(Guid id);
        Task<int> CountAsync();
        Task<List<T>> GetFilteredListAsync(Expression<Func<T, bool>> predicate);


    }
}
