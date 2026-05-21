using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Reflection.Metadata.Ecma335;
using WaifuCLI.Core.CLI;
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
        
        
        public static async Task<int> Main(string[] args)
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
            services.AddSingleton<ICLI, CLI>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ICLI cli = serviceProvider.GetRequiredService<ICLI>();
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
