using Blog.Entity;
using Blogger.Buisiness.Abstract;
using FinishBlog.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinishBlog.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPostService _postService;  // Postları çekmek için servis
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfileController(UserManager<ApplicationUser> userManager, IPostService postService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _postService = postService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            // Giriş yapmış kullanıcının Id'sini alıyoruz
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge(); // Kullanıcı giriş yapmamışsa login sayfasına yönlendirir
            }

            // Kullanıcının bloglarını çekiyoruz
            var posts = await _postService.GetPostsByUserIdAsync(user.Id);

            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null || post.IsDeleted)
            {
                return NotFound();
            }

            var model = new EditBlogPostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ExistingImageUrl = post.ImageUrl
            };

            return View(model);
        }
       

    }
}
