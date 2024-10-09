using SearchPic_V2.Services;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SaveImageAsync(string fileName, string blobUrl, string tags)
        {
            var tagList = tags.Split(',').Select(tag => new Tag { TagName = tag.Trim() }).ToList();
            var image = new Image
            {
                FileName = fileName,
                BlobUrl = blobUrl,
                Tags = tagList
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Image>> SearchImagesByKeywordsAsync(string keywords)
        {
            var keywordList = keywords.Split(',').Select(k => k.Trim()).ToList();
            var query = _context.Images
                .Include(i => i.Tags)
                .Where(i => i.Tags.Any(tag => keywordList.Contains(tag.TagName)))
                .AsQueryable();
            return await query.ToListAsync();
        }
    }
}
