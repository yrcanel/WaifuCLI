using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Core.CLI
{
    class CLI : ICLI
    {
        private readonly IEngine engine;
        public CLI(IEngine engine)      
        {
            this.engine = engine;
        }   

        public async Task<int> StartCli(string[] args)
        {
            Option<string[]> tags = new("--tags")
            {
                Description = "List of tags for finding an image with those tags; Default: random image"
            };
            Option<bool> isNsfw = new("--IsNsfw")
            {
                Description = "Turn on|off 18+ content; Default: All"
            };
            Option<string> path = new("--outputPath")
            {
                Required = true,
                Description = "Path where the image will be saved"
            };
            RootCommand rootCommand = new("Simple app for downloading waifu images");
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
                await engine.GetAndDownloadImageAsync(parseResult.GetValue(tags), parseResult.GetValue(isNsfw), outputPath);
                return 0;

            });
            ParseResult parseResult = rootCommand.Parse(args);
            return await parseResult.InvokeAsync();

        }
    }
}
