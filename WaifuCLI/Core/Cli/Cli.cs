using System.CommandLine;
using System.Security.Cryptography.X509Certificates;
using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Core.Cli
{
    class Cli : ICli
    {
        private readonly IEngine _engine;
        public Cli(IEngine engine)      
        {
            _engine = engine;
        }   

        public async Task<int> StartCli(string[] args)
        {
            using CancellationTokenSource cts = new CancellationTokenSource();

            Option<string[]?> tags = new("--tags")
            {
                Description = "List of tags for finding an image with those tags; Default: random image"
            };
            Option<bool?> isNsfw = new("--IsNsfw")
            {
                Description = "Turn on|off 18+ content; Default: All"
            };
            Option<string> path = new("--outputPath")
            {
                Required = true,
                Description = "Path where the image will be saved"
            };
            RootCommand rootCommand = new("Simple app for downloading waifu images");
            Command getTags = new("get-tags", "Display a list of awailable tags");
            rootCommand.Subcommands.Add(getTags);

            rootCommand.Add(path);
            rootCommand.Add(isNsfw);
            rootCommand.Add(tags);
            rootCommand.SetAction(async parseResult =>
            {
                string? outputPath = parseResult.GetValue(path);
                if (outputPath is null)
                {
                    return 1;
                }
                try
                {
                    string message = "Finding and downloading an image...";
                    Task spinnerTask = Utils.Utils.StartSpinner(message, cts.Token);
                    await _engine.GetAndDownloadImageAsync(parseResult.GetValue(tags), parseResult.GetValue(isNsfw), outputPath);
                    cts.Cancel();
                    await spinnerTask;
                    Console.Write($"\r✓ {message}");
                }
                catch (CliException cex)
                {
                    Console.Error.Write($"\r{cex.Message}");
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex}");
                    return 99;
                }
                return 0;

            });
            getTags.SetAction(async parseResult =>
            {
                try
                {
                    await _engine.GetAndPrintTagsAsync();
                }
                catch (CliException cex)
                {
                    Console.Error.WriteLine(cex.Message);
                    return 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex}");
                    return 99;
                }
                return 0;
            });
            ParseResult parseResult = rootCommand.Parse(args);
            return await parseResult.InvokeAsync();

        }
    }
}
