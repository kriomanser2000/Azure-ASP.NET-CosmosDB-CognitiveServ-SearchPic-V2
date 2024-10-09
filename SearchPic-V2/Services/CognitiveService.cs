using SearchPic_V2.Services;
using SearchPic_V2.Models;

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
                var moderationResult = await response.Content.ReadAsAsync<ModerationResult>();
                return moderationResult;
            }
        }
    }
}
