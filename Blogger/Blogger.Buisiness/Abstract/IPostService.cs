using Blog.Entity.BlogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness.Abstract
{
    public interface IPostService:IGenericService<Post>
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetPostsByUserIdAsync(Guid userId);
        Task<Post> GetPostByIdAsync(Guid postId);
    }
}
