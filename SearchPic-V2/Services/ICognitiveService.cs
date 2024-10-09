using SearchPic_V2.Services;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public interface ICognitiveService
    {
        Task<ModerationResult> CheckImageAsync(IFormFile file);
    }
}
