using Blog.Entity.BlogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId);
        Task AddCommenAsync(Comment comment);
    }
}
