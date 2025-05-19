using Blog.DataAccess.Abstraction;
using Blog.DataAccess.Context;
using Blog.DataAccess.Repository;
using Blog.Entity;
using Blog.Entity.BlogEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.EntityFrameWork
{
    public class EFUserDal : GenericRepository<ApplicationUser>, IUserDal
    {
        private readonly BlogDbContext dbContext;
        public EFUserDal(BlogDbContext context, BlogDbContext dbContext) : base(context)
        {
            this.dbContext = dbContext;
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
