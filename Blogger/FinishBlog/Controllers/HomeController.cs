using System.Diagnostics;
using Blogger.Buisiness.Abstract;
using FinishBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinishBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 7;

            var allPosts = await _postService.GetAllPostsAsync();

            if (page < 1) page = 1;

            int totalPosts = allPosts.Count;
            int totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);

            if (page > totalPages) page = totalPages;

            var postsToShow = allPosts
                .OrderByDescending(p => p.CreatedTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new BlogIndexViewModel
            {
                Posts = postsToShow,
                CurrentPage = page,
                TotalPages = totalPages
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(CreateBlogPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Validation hatalar� varsa, formu tekrar g�ster
                return View(model);
            }

            // TODO: Veritaban�na kaydetme i�lemi burada yap�lacak.
            // �imdilik sadece ba�ar�l� sayfas�na y�nlendirelim veya anasayfaya.
            return RedirectToAction("Index");
        }

    }
}
