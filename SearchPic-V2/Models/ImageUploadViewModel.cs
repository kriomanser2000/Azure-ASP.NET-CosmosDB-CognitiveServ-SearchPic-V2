using Microsoft.AspNetCore.Http;

namespace SearchPic_V2.Models
{
    public class ImageUploadViewModel
    {
        public IFormFile ImageFile { get; set; }
    }
}
