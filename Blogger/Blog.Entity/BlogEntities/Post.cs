using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entity.BlogEntities
{
    public class Post:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? CoverImagePath { get; set; }
        public bool IsPublished { get; set; } = false;
        public bool AllowComments { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public string  ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
