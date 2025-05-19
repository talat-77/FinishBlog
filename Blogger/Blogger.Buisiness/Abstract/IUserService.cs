using Blog.Entity;
using Blog.Entity.BlogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness.Abstract
{
    public interface IUserService:IGenericService<ApplicationUser>
    {
        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task AddUserAsync(ApplicationUser user);
    }
}
