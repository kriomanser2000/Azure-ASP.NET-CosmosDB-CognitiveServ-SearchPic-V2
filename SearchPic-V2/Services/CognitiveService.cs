using SearchPic_V2.Services;
using SearchPic_V2.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SearchPic_V2.Services
{
    public class CognitiveService : ICognitiveService
    {
        private readonly HttpClient _httpClient;
        private readonly string _cognitiveServiceUrl = "https://your-cognitive-service-endpoint/contentmoderator/moderate/v1.0/ProcessImage";
        public CognitiveService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ModerationResult> CheckImageAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var content = new StreamContent(stream);
                content.Headers.Add("Content-Type", file.ContentType);
                var response = await _httpClient.PostAsync(_cognitiveServiceUrl, content);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                var moderationResult = await JsonSerializer.DeserializeAsync<ModerationResult>(responseStream);
                return moderationResult;
            }
        }
    }
}