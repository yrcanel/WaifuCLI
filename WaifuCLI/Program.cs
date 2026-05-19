using Microsoft.Extensions.DependencyInjection;
using WaifuCLI.Core.Engine;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Infrastructure.ApiClient;
using WaifuCLI.Infrastructure.ImageClient;
using WaifuCLI.Infrastructure.ImageDownloader;
using WaifuCLI.Infrastructure.JSONDeserializer;
using WaifuCLI.Infrastructure.URLBuilder;

namespace WaifuCLI
{
    class Program
    {
        
        
        public static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.waifu.im/images?");
            });
            services.AddHttpClient<ImageClient>();
            services.AddSingleton<IURLBuilder, URLBuilder>();
            services.AddSingleton<IJSONDeserializer, JSONDeserializer>();
            services.AddSingleton<IImageClient, ImageClient>();
            services.AddSingleton<IImageDownloader, ImageDownloader>();
            services.AddSingleton<IEngine, Engine>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IEngine engine = serviceProvider.GetRequiredService<IEngine>();
            try
            {
                await engine.GetAndDownloadImageAsync(["waifu", "genshin-impact"], false, "C:/Users/vd682/OneDrive/Pictures");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Unexpected error: {ex}");
                Environment.Exit(99);
            }
            


        }
    }
}
