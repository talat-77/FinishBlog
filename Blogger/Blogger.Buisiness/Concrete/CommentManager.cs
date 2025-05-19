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
    public class CommentManager : GenericManager<Comment>, ICommentService
    {
        private readonly ICommentDal _commentDal;
        public CommentManager(IGenericDal<Comment> genericdal, ICommentDal commentDal) : base(genericdal)
        {
            _commentDal = commentDal;
        }

        public async Task AddCommenAsync(Comment comment)
        {
            await _commentDal.CreateAsync(comment);
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _commentDal.GetCommentsByPostIdAsync(postId);
        }
    }
}
