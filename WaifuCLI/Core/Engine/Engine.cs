using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Exceptions;
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
        }

        public async Task  GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir)
        {
            try
            {
                if (!Directory.Exists(outputDir))
                {
                    Console.WriteLine("Specified directory isn't found");
                    Environment.Exit(0);
                }
                using Stream waifuStream = await apiClient.GetResponseStreamAsync(tags, IsNsfw);
                WaifuImage? waifuImageObject = await jsonDeserializer.DeserializeJsonAsync(waifuStream);
                if (waifuImageObject is null)
                {
                    Console.WriteLine("No images with specified tags were found");
                    Environment.Exit(0);
                }
                using Stream imageStream = await imageClient.GetImageStreamAsync(waifuImageObject.url);
                await imageDownloader.DownloadImageAsync($"{outputDir}/{waifuImageObject.id}.png", imageStream);

            }
            catch (CliException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            
        }
    }
}
