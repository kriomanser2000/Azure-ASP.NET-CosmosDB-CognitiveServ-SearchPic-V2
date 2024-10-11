using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public class ImageService : IImageService
    {
        private readonly List<Image> _images = new List<Image>();
        public async Task<IEnumerable<Image>> SearchImagesAsync(string keywords)
        {
            return await Task.FromResult(_images.Where(img =>
                img.Tags.Any(tag => tag.TagName.Contains(keywords))));
        }
        public async Task UploadImageAsync(ImageUploadViewModel model)
        {
            var newImage = new Image
            {
                ImageId = _images.Count + 1,
                FileName = model.ImageFile.FileName,
                BlobUrl = "/images/" + model.ImageFile.FileName,
                Tags = new List<Tag> { new Tag { TagName = "ExampleTag" } }
            };
            _images.Add(newImage);
            await Task.CompletedTask;
        }
        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await Task.FromResult(_images.FirstOrDefault(img => img.ImageId == id));
        }
        public async Task SaveImageAsync(string fileName, string blobUrl, string tags)
        {
            var newImage = new Image
            {
                ImageId = _images.Count + 1,
                FileName = fileName,
                BlobUrl = blobUrl,
                Tags = tags.Split(',').Select(tag => new Tag { TagName = tag.Trim() }).ToList()
            };
            _images.Add(newImage);
            await Task.CompletedTask;
        }
    }
}