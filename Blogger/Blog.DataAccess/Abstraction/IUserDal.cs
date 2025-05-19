using Blog.Entity;
using Blog.Entity.BlogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DataAccess.Abstraction
{
    public interface IUserDal : IGenericDal<ApplicationUser>
    {

        Task<ApplicationUser> GetByUsernameAsync(string username);
        Task<ApplicationUser> GetByEmailAsync(string email);
        Task AddUserAsync(ApplicationUser user);
    }
}
