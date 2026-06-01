using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.ImageClient
{
    public class ImageClient : IImageClient
    {
        private readonly HttpClient _httpClient;
        public ImageClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> GetImageStreamAsync(Uri url)
        {
            HttpResponseMessage ImageResponse = await _httpClient.GetAsync(url);
            try
            {
                ImageResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException($"HTTP {(int)ImageResponse.StatusCode} : {ImageResponse.ReasonPhrase}", (int)ImageResponse.StatusCode, ex);
            }

            Stream ImageStream = await ImageResponse.Content.ReadAsStreamAsync();

            return ImageStream;
        }
    }
}
