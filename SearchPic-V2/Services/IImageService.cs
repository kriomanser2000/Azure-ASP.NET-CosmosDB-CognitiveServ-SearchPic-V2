using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public interface IImageService
    {
        Task<IEnumerable<Image>> SearchImagesAsync(string keywords);
        Task UploadImageAsync(ImageUploadViewModel model);
        Task<Image> GetImageByIdAsync(int id);
        Task SaveImageAsync(string fileName, string blobUrl, string tags);
    }
}