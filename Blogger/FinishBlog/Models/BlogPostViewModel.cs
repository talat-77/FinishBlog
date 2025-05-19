namespace FinishBlog.Models
{
    public class BlogPostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new();
        public string ImageUrl { get; set; }
    }
}
