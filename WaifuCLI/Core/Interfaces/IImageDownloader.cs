using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface IImageDownloader
    {
        Task DownloadImageAsync(string imagePath, Stream ImageStream);
    }
}
