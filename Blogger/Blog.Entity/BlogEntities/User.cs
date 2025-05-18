using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.BlogEntities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; } = false;

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
