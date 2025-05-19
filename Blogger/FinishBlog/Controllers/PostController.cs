using Blog.Entity;
using Blog.Entity.BlogEntities;
using Blogger.Buisiness.Abstract;
using FinishBlog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinishBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(IPostService postService, IWebHostEnvironment webHostEnvironment, ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
            _commentService = commentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreateBlogPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? imagePath = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "posts");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(fileStream);

                imagePath = "/images/posts/" + uniqueFileName;
            }

            // Giriş yapmış kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Kullanıcı giriş yapmamışsa login sayfasına yönlendir
                return Challenge();
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Content = model.Content,
                CoverImagePath = imagePath,
                IsPublished = true,
                CreatedTime = DateTime.UtcNow,
                IsDeleted = false,
                ImageUrl = imagePath,
                UserId = user.Id 
            };

            await _postService.TCreateAsync(post);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var postEntity = await _postService.TGetByIdAsync(id);
            if (postEntity == null || postEntity.IsDeleted)
                return NotFound();

            var comments = await _commentService.GetCommentsByPostIdAsync(id);

            var model = new BlogPostViewModel
            {
                Id = postEntity.Id,
                Title = postEntity.Title,
                PublishDate = postEntity.CreatedTime,
                Content = postEntity.Content,
                ImageUrl = postEntity.ImageUrl,
                Comments = comments.Select(c => new CommentViewModel
                {
                    Name = c.AuthorName,
                    Text = c.Content,
                    Date = c.CreatedTime
                }).OrderByDescending(c => c.Date).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Guid postId, string commentText)
        {
            // Yorum metni boş mu kontrol et
            if (string.IsNullOrWhiteSpace(commentText))
            {
                ModelState.AddModelError(string.Empty, "Yorum metni zorunludur.");
                return RedirectToAction("Details", new { id = postId });
            }

            // Giriş yapmış kullanıcıyı al
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Kullanıcı giriş yapmamışsa yorum yapmasın
                return Forbid(); // veya RedirectToAction("Login", "Account");
            }

            var postEntity = await _postService.TGetByIdAsync(postId);
            if (postEntity == null || postEntity.IsDeleted)
                return NotFound();

            await _commentService.AddCommenAsync(new Comment
            {
                Id = Guid.NewGuid(),
                AuthorName = user.NameSurname, // veya user.UserName (kendi projenin user modeline göre)
                Content = commentText,
                CreatedTime = DateTime.UtcNow,
                PostId = postId,
                UserId = user.Id
            });

            return RedirectToAction("Details", new { id = postId });
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.IsDeleted)
                return NotFound();

            var model = new EditBlogPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ExistingImageUrl = post.ImageUrl
            };

            return View("EditPost", model); // bu view dosyasını açacak
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBlogPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var post = await _postService.GetPostByIdAsync(model.Id);
            if (post == null || post.IsDeleted)
            {
                return NotFound();
            }

            post.Title = model.Title;
            post.Content = model.Content;
            post.UpdatedTime = DateTime.UtcNow;

            // Yeni resim yüklendiyse
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "posts");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(fileStream);

                post.ImageUrl = "/images/posts/" + uniqueFileName;
            }

            await _postService.TUpdateAsync(post);

            return RedirectToAction("Index", "Profile"); // kendi postlarını listeleyeceğin sayfaya dönüş
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var post = await _postService.TGetByIdAsync(id);
            if (post == null)
                return NotFound();

            await _postService.TDelete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
