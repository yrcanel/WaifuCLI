using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;

namespace WaifuCLI.Core.Engine
{
    class Engine : IEngine
    {
        private readonly IApiClient _apiClient;
        private readonly IImageClient _imageClient;
        private readonly IImageDownloader _imageDownloader;
        private readonly IJsonDeserializer _jsonDeserializer;
        public Engine(IApiClient apiClient, IImageClient imageClient, IImageDownloader imageDownloader, IJsonDeserializer jsonDeserializer)
        {
            _apiClient = apiClient;
            _imageClient = imageClient;
            _imageDownloader = imageDownloader;
            _jsonDeserializer = jsonDeserializer;       
        }

        public async Task  GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir)
        { 
                using Stream waifuStream = await _apiClient.GetResponseStreamAsync(tags, IsNsfw);
                WaifuImage? waifuImageObject = await _jsonDeserializer.DeserializeJsonAsync(waifuStream);
                if (waifuImageObject is null)
                {
                    Console.WriteLine("No images with specified tags were found");
                    return;
                }
                using Stream imageStream = await _imageClient.GetImageStreamAsync(waifuImageObject.url);
                await _imageDownloader.DownloadImageAsync($"{outputDir}/{waifuImageObject.id}.png", imageStream);
            
        }
    }
}
