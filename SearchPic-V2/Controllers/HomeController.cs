using Microsoft.AspNetCore.Mvc;
using SearchPic_V2.Models;
using SearchPic_V2.Services;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SearchPic_V2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageService _imageService;

        public HomeController(IImageService imageService)
        {
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            return View(new ImageSearchViewModel());
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keywords)
        {
            var images = await _imageService.SearchImagesAsync(keywords);
            return View("SearchResults", images);
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View(new ImageUploadViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Upload(ImageUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _imageService.UploadImageAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ViewImage(int id)
        {
            var image = await _imageService.GetImageByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return View(image);
        }
    }
}