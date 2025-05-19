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
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly BlogDbContext _dbContext;

        public EFCommentDal(BlogDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _dbContext.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedTime)
                .ToListAsync();
        }

       
    }
}
