using Blog.Entity.BlogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Abstraction
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
        Task AddCommentAsync(Comment comment);
    }
}
