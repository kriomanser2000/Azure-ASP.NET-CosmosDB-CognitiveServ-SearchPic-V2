using Microsoft.Azure.Cosmos;
using SearchPic_V2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchPic_V2.Services;

namespace SearchPic_V2.Models
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;
        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }
        public async Task<IEnumerable<Image>> GetImagesAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Image>(new QueryDefinition(queryString));
            List<Image> results = new List<Image>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
        public async Task AddImageAsync(Image image)
        {
            await _container.CreateItemAsync(image, new PartitionKey(image.ImageId.ToString()));
        }
        public async Task<Image> GetImageAsync(string id)
        {
            try
            {
                ItemResponse<Image> response = await _container.ReadItemAsync<Image>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}