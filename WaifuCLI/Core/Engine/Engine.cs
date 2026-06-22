using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using WaifuCLI.Utils;

namespace WaifuCLI.Core.Engine
{
    class Engine : IEngine
    {
        private readonly IApiClient _apiClient;
        private readonly IImageClient _imageClient;
        private readonly IImageDownloader _imageDownloader;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly SemaphoreSlim _semaphore;
        public Engine(IApiClient apiClient, IImageClient imageClient, IImageDownloader imageDownloader, IJsonDeserializer jsonDeserializer, SemaphoreSlim semaphore)
        {
            _apiClient = apiClient;
            _imageClient = imageClient;
            _imageDownloader = imageDownloader;
            _jsonDeserializer = jsonDeserializer;  
            _semaphore = semaphore;
        }

        public async Task GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir, int? amount)
        {
            using Stream waifuStream = await _apiClient.GetResponseStreamAsync(tags, IsNsfw, amount);
            WaifuImage[] waifuImageObjects = await _jsonDeserializer.DeserializeImageJsonAsync(waifuStream);
            List<Task> downloadImagesTasks = new List<Task>();
            foreach (WaifuImage image in waifuImageObjects)
            {
                downloadImagesTasks.Add(ProcessImageObject(image, outputDir));
            }
            await Task.WhenAll(downloadImagesTasks);
        }
        public async Task GetAndPrintTagsAsync()
        {
            using Stream tagsStream = await _apiClient.GetTagsStreamAsync();
            Tag[] tags = await _jsonDeserializer.DeserializeTagJsonAsync(tagsStream);
            await Utils.Utils.PrintTagsAsync(tags);
        }
        private async Task ProcessImageObject(WaifuImage image, string outputDir)
        {
            await _semaphore.WaitAsync();
            try
            {
                using Stream imageStream = await _imageClient.GetImageStreamAsync(image.url);
                await _imageDownloader.DownloadImageAsync($"{outputDir}/{image.id}.png", imageStream);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
