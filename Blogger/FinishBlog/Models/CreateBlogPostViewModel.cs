using System.ComponentModel.DataAnnotations;

namespace FinishBlog.Models
{
    public class CreateBlogPostViewModel
    {
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik zorunludur.")]
        public string Content { get; set; }

        // Resim dosyası için
        public IFormFile ImageFile { get; set; }
    }
}
