using Blog.DataAccess.Abstraction;
using Blog.DataAccess.EntityFrameWork;
using Blog.Entity.BlogEntities;
using Blogger.Buisiness.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness.Concrete
{
    public class PostManager : GenericManager<Post>, IPostService
    {
        private readonly IPostDal _postDal;
        public PostManager(IGenericDal<Post> genericdal, IPostDal postDal) : base(genericdal)
        {
            _postDal = postDal;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _postDal.GetAllPostsAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
           return await _postDal.GetPostByIdAsync(postId);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            return await _postDal.GetPostsByUserIdAsync(userId);
        }
    }
}
