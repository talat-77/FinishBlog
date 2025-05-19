using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.BlogEntities
{
    public class Comment:BaseEntity
    {
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; } = false;

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
