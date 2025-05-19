using Blog.DataAccess.Abstraction;
using Blog.DataAccess.Context;
using Blog.DataAccess.Repository;
using Blog.Entity.BlogEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.EntityFrameWork
{
    public class EFPostDal : GenericRepository<Post>, IPostDal
    {
        private readonly BlogDbContext dbContext;
        public EFPostDal(BlogDbContext context, BlogDbContext dbContext) : base(context)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await dbContext.Posts.ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await dbContext.Posts
                 .AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == postId && !p.IsDeleted);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            return await dbContext.Posts
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
