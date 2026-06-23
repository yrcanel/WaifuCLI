using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using WaifuCLI.Infrastructure.ImageDownloader;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Tests
{
    public class ImageDownloaderTest
    {
        [Fact]
        public async Task DownloadImageAsync_GoodPath_ShouldCreate()
        {
            byte[] data = [1, 2, 3, 4, 5];
            using MemoryStream stream = new MemoryStream(data);
            string filepath = Path.GetTempFileName();
            File.Delete(filepath);
            ImageDownloader imageDownloader = new ImageDownloader();
            await imageDownloader.DownloadImageAsync(filepath, stream);
            byte[] result = await File.ReadAllBytesAsync(filepath);
            Assert.Equal(data, result);
            
        }
        [Fact]
        public async Task DownloadImageAsync_UnauthorizedAccess_ShouldThrow()
        {
            byte[] data = [1, 2, 3, 4, 5];
            using MemoryStream stream = new MemoryStream(data);
            string filepath = Path.GetTempFileName();
            File.Delete(filepath);
            ImageDownloader imageDownloader = new ImageDownloader();
            await Assert.ThrowsAsync<DownloadException>(async () =>
            {
                await imageDownloader.DownloadImageAsync("C:/Windows", stream);
            });
        }

    }
}
