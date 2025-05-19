using Blog.Entity.BlogEntities;

namespace FinishBlog.Models
{
    public class BlogIndexViewModel
    {
        public List<Blog.Entity.BlogEntities.Post> Posts { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
