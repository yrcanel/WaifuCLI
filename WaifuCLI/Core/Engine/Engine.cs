using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;

namespace WaifuCLI.Core.Engine
{
    class Engine : IEngine
    {
        private readonly IApiClient apiClient;
        private readonly IImageClient imageClient;
        private readonly IImageDownloader imageDownloader;
        private readonly IJSONDeserializer jsonDeserializer;
        public Engine(IApiClient apiClient, IImageClient imageClient, IImageDownloader imageDownloader, IJSONDeserializer jsonDeserializer)
        {
            this.apiClient = apiClient;
            this.imageClient = imageClient;
            this.imageDownloader = imageDownloader;
            this.jsonDeserializer = jsonDeserializer;
            Console.WriteLine("Initialized the Engine");
        }

        public async Task  GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir)
        {
            Console.WriteLine("Initialized the Processing");
            using Stream waifuStream = await apiClient.GetResponseStreamAsync(tags, IsNsfw);
            WaifuImage waifuImageObject = await jsonDeserializer.DeserializeJsonAsync(waifuStream);
            Console.WriteLine($"Downloading {waifuImageObject.url}");
            using Stream imageStream = await imageClient.GetImageStreamAsync(waifuImageObject.url);
            await imageDownloader.DownloadImageAsync($"{outputDir}/{waifuImageObject.id}.png", imageStream);
        }
    }
}
