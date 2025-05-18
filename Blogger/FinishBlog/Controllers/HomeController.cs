using System.Diagnostics;
using FinishBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinishBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult PostDetails()
        {
            var model = new BlogPostViewModel
            {
                Id = 1,
                Title = "�rnek Ba�l�k",
                PublishDate = DateTime.Now,
                Content = "Burada blog i�eri�i yer alacak.",
                Comments = new List<CommentViewModel>()
        {
            new CommentViewModel { Name = "Ali", Date = DateTime.Now, Text = "Harika yaz�!" },
            new CommentViewModel { Name = "Ay�e", Date = DateTime.Now, Text = "Te�ekk�rler." }
        }
            };

            return View(model);
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
