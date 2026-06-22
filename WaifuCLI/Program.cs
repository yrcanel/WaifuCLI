using Microsoft.Extensions.DependencyInjection;
using WaifuCLI.Core.Cli;
using WaifuCLI.Core.Engine;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Infrastructure.ApiClient;
using WaifuCLI.Infrastructure.ImageClient;
using WaifuCLI.Infrastructure.ImageDownloader;
using WaifuCLI.Infrastructure.JsonDeserializer;
using WaifuCLI.Infrastructure.UrlBuilder;

namespace WaifuCLI
{
    class Program
    {
        
        
        public static async Task<int> Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient<IApiClient, ApiClient>();
            services.AddHttpClient<ImageClient>();
            services.AddSingleton<UriBuilder>(_ =>
            {
                return new UriBuilder("https://api.waifu.im/");
            });
            services.AddSingleton<SemaphoreSlim>(new SemaphoreSlim(3));
            services.AddSingleton<IUrlBuilder, UrlBuilder>();
            services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
            services.AddSingleton<IImageClient, ImageClient>();
            services.AddSingleton<IImageDownloader, ImageDownloader>();
            services.AddSingleton<IEngine, Engine>();
            services.AddSingleton<ICli, Cli>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ICli cli = serviceProvider.GetRequiredService<ICli>();
            int exitCode = await cli.StartCli(args);
            return exitCode;
            
        }
    }
}
