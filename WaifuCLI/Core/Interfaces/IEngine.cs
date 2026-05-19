using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Models;

namespace WaifuCLI.Core.Interfaces
{
    interface IEngine
    {
        Task GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir);
        

    }
}
