using System.CommandLine;
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

            Option<string[]?> tagsOption = new("--tags")
            {
                Description = "Tag used to find images (can be used multiple times). Default: random image"
            };
            Option<bool?> isNsfwOption = new("--IsNsfw")
            {
                Description = "Enable/disable 18+ content. Default: true"
            };
            Option<FileInfo> pathOption = new("--outputPath")
            {
                Required = true,
                Description = "Path where the image will be saved"
            };
            pathOption.AcceptExistingOnly();
            Option<int?> amountOption = new("--amount")
            {
                Description = "Number of images to download. Default: 1"
            };
            amountOption.Validators.Add(result =>
            {
                int? amount = result.GetValue(amountOption);
                if (amount is not null && (amount < 1 || amount > 100))
                {
                    result.AddError("Amount should be a positive integer between 1 and 100");
                }
            });
            RootCommand rootCommand = new("Simple app for downloading waifu images");
            Command getTags = new("get-tags", "Display a list of awailable tags");
            rootCommand.Subcommands.Add(getTags);

            rootCommand.Add(pathOption);
            rootCommand.Add(isNsfwOption);
            rootCommand.Add(tagsOption);
            rootCommand.Add(amountOption);
            rootCommand.SetAction(async parseResult =>
            {
                FileInfo? outputPath = parseResult.GetValue(pathOption);
                if (outputPath is null)
                {
                    return 1;
                }
                int? amount = parseResult.GetValue(amountOption);
                string message;
                if (amount is null || amount == 1)
                {
                    message = "Finding and downloading an image...";
                }
                else
                {
                    message = "Finding and downloading images...";
                }
                try
                {
                    
                    Task spinnerTask = Utils.Utils.StartSpinner(message, cts.Token);
                    await _engine.GetAndDownloadImageAsync(parseResult.GetValue(tagsOption), parseResult.GetValue(isNsfwOption), outputPath.FullName, amount);

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
