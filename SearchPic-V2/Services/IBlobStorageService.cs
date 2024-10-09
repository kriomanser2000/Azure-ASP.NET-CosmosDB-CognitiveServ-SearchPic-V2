using SearchPic_V2.Services;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public interface IBlobStorageService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
