using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public interface IImageService
    {
        Task SaveImageAsync(string fileName, string bloburl, string tags);
        Task<IEnumerable<Image>> SearchImagesByKeywordsAsync(string keywords);
    }
}
