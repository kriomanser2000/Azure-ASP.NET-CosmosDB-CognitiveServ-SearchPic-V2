using Microsoft.AspNetCore.Mvc;
using SearchPic_V2.Services;
using SearchPic_V2.Models;

namespace SearchPic_V2.Controllers
{
    public class UploadController : Controller
    {
        private readonly IBlobStorageService _blobStorageService;
        private readonly ICognitiveService _cognitiveService;
        private readonly IImageService _imageService;
        public UploadController(IBlobStorageService blobStorageService, ICognitiveService cognitiveService, IImageService imageService)
        {
            _blobStorageService = blobStorageService;
            _cognitiveService = cognitiveService;
            _imageService = imageService;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string tags)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select an image to upload.");
                return View("Upload");
            }
            var moderationResult = await _cognitiveService.CheckImageAsync(file);
            if (!moderationResult.IsApproved)
            {
                ModelState.AddModelError("", "The image did not pass content moderation.");
                return View("Upload");
            }
            var blobUrl = await _blobStorageService.UploadImageAsync(file);
            await _imageService.SaveImageAsync(file.FileName, blobUrl, tags);
            return RedirectToAction("UploadSuccess");
        }
        public IActionResult UploadSuccess()
        {
            return View();
        }
    }
}