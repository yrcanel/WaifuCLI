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
            services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.waifu.im/images?");
            });
            services.AddHttpClient<ImageClient>();
            services.AddSingleton<IUrlBuilder, UrlBuilder>();
            services.AddSingleton<IJsonDeserializer, JsonDeserializer>();
            services.AddSingleton<IImageClient, ImageClient>();
            services.AddSingleton<IImageDownloader, ImageDownloader>();
            services.AddSingleton<IEngine, Engine>();
            services.AddSingleton<ICli, Cli>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ICli cli = serviceProvider.GetRequiredService<ICli>();
            try
            {
                int exitCode = await cli.StartCli(args);
                return exitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex}");
                Environment.Exit(99);
            }
            return 0;
        }
    }
}
