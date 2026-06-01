using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Infrastructure.ImageClient;


namespace WaifuCLI.Tests
{
    public class ImageClientTest
    {
        [Fact]
        public async Task GetImageStreamAsync_SuccessfulCode_ShouldReturnStream()
        {
            byte[] bytes = [1, 2, 3, 4, 5, 6, 7];
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
            };
            HttpMessageHandler handler = new FakeHandler(httpResponse);
            HttpClient client = new HttpClient(handler);
            ImageClient imageClient = new ImageClient(client);
            Stream respStream = await imageClient.GetImageStreamAsync(new Uri("https://cdn.waifu.im/2065"));
            using MemoryStream ms = new MemoryStream();
            await respStream.CopyToAsync(ms);
            Assert.Equal(bytes, ms.ToArray());
        }

        [Fact]
        public async Task GetImageStreamAsync_UnsuccessfulCode_ShouldThrow()
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                ReasonPhrase = "Not Found"
            };
            HttpMessageHandler handler = new FakeHandler(httpResponse);
            HttpClient client = new HttpClient(handler);
            ImageClient imageClient = new ImageClient(client);
            ApiException ex = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await imageClient.GetImageStreamAsync(new Uri("https://cdn.waifu.im/xxxx"));
            });
            Assert.Equal(404, ex.StatusCode);
            Assert.Equal("HTTP 404 : Not Found", ex.Message);
            
        }
    }
}
