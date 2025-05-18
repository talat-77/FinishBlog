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
                Title = "Örnek Baþlýk",
                PublishDate = DateTime.Now,
                Content = "Burada blog içeriði yer alacak.",
                Comments = new List<CommentViewModel>()
        {
            new CommentViewModel { Name = "Ali", Date = DateTime.Now, Text = "Harika yazý!" },
            new CommentViewModel { Name = "Ayþe", Date = DateTime.Now, Text = "Teþekkürler." }
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
                // Validation hatalarý varsa, formu tekrar göster
                return View(model);
            }

            // TODO: Veritabanýna kaydetme iþlemi burada yapýlacak.
            // Þimdilik sadece baþarýlý sayfasýna yönlendirelim veya anasayfaya.
            return RedirectToAction("Index");
        }

    }
}
