using Blog.Entity.BlogEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string NameSurname { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedTime { get; set; } = DateTime.UtcNow;

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
