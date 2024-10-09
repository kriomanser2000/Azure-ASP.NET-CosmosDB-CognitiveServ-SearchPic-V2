using Microsoft.AspNetCore.Mvc;
using SearchPic_V2.Models;
using SearchPic_V2.Services;
using System.Diagnostics;

namespace SearchPic_V2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;
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
        [HttpPost]
        public async Task<IActionResult> Search(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
            {
                ModelState.AddModelError("", "Please enter search keywords.");
                return View("Index");
            }
            var images = await _imageService.SearchImagesByKeywordsAsync(keywords);
            return View("SearchResults", images);
        }
    }
}
