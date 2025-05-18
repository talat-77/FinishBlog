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

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
