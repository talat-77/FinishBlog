using System.ComponentModel.DataAnnotations;

namespace FinishBlog.Models
{
    public class EditBlogPostViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
