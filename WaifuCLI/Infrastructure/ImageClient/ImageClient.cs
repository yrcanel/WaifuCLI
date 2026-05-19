using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.ImageClient
{
    class ImageClient : IImageClient
    {
        private readonly HttpClient httpClient;
        public ImageClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Stream> GetImageStreamAsync(Uri url)
        {
            HttpResponseMessage ImageResponse = await httpClient.GetAsync(url);
            ImageResponse.EnsureSuccessStatusCode();
            Stream ImageStream = await ImageResponse.Content.ReadAsStreamAsync();

            return ImageStream;
        }
    }
}
