using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.ImageDownloader
{
    class ImageDownloader : IImageDownloader
    {
        public async Task DownloadImageAsync(string imagePath, Stream ImageStream)
        {

            await using FileStream output = File.Create(imagePath);
            await ImageStream.CopyToAsync(output);
        }
    }
}
