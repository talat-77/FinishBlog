using Blog.DataAccess.Abstraction;
using Blog.Entity;
using Blog.Entity.BlogEntities;
using Blogger.Buisiness.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Buisiness.Concrete
{
    public class UserManager : GenericManager<ApplicationUser>, IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IGenericDal<ApplicationUser> genericDal, IUserDal userDal) : base(genericDal)
        {
            _userDal = userDal;
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _userDal.GetByUsernameAsync(username);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _userDal.GetByEmailAsync(email);
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            await _userDal.AddUserAsync(user);
        }
    }
}
