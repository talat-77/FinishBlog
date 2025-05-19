using Blogger.Buisiness.Abstract;
using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Blog.DataAccess.Abstraction;

namespace Blogger.Buisiness.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {

        private readonly IGenericDal<T> _genericdal;

        public GenericManager(IGenericDal<T> genericdal)
        {
            _genericdal = genericdal;
        }

        public async Task<int> TCountAsync()
        {
            return await _genericdal.CountAsync();
        }

        public async Task TCreateAsync(T entity)
        {
            await _genericdal.CreateAsync(entity);
        }

        public async Task TDelete(Guid id)
        {
            await _genericdal.Delete(id);
        }

        public async Task<T> TGetByIdAsync(Guid id)
        {
            return await _genericdal.GetByIdAsync(id);
        }

        public async Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _genericdal.GetFilteredListAsync(predicate);
        }

        public async Task<List<T>> TGetListAsnc()
        {
            return await _genericdal.GetListAsnc();
        }

        public async Task TUpdateAsync(T entity)
        {
            await _genericdal.UpdateAsync(entity);
        }
    }
}
