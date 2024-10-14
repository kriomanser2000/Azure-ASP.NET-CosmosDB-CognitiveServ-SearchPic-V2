using System.Collections.Generic;
using System.Threading.Tasks;
using SearchPic_V2.Models;

namespace SearchPic_V2.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<Image>> GetImagesAsync(string queryString);
        Task AddImageAsync(Image image);
        Task<Image> GetImageAsync(string id);
    }
}