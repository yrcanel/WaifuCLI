using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WaifuCLI.Infrastructure.ApiClient;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Tests
{
    public class ApiClientTest
    {
        [Fact]
        public async Task GetResponseStreamAsync_SuccessfulCode_ShouldReturnStream()
        {
            string json = """
            {
            	"items": [
            		{
            			"id": 7783,
            			"isNsfw": false,
            			"url": "https://cdn.waifu.im/7783.jpg"
                    }
                ]
            }
            """;
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            };
            HttpMessageHandler handler = new FakeHandler(httpResponse);
            HttpClient client = new HttpClient(handler);
            IUrlBuilder builder = Substitute.For<IUrlBuilder>();
            builder.BuildUrlWithTags(Arg.Any<string[]?>(), Arg.Any<bool?>()).Returns("https://api.waifu.im/images?IncludedTags=ecchi&isNsfw=True");
            ApiClient apiClient = new ApiClient(client, builder);
            Stream respStream = await apiClient.GetResponseStreamAsync(["ecchi"], true);

            Assert.NotNull(respStream);
        }

        [Fact]
        public async Task GetResponseStreamAsync_UnsuccessfulCode_ShouldThrow()
        {
            
            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                ReasonPhrase = "Bad Request"
            };
            HttpMessageHandler handler = new FakeHandler(httpResponse);
            HttpClient client = new HttpClient(handler);
            IUrlBuilder builder = Substitute.For<IUrlBuilder>();
            builder.BuildUrlWithTags(Arg.Any<string[]?>(), Arg.Any<bool?>()).Returns("https://api.waifu.im/images?Tag=ecchi&isNsfw=True");
            ApiClient apiClient = new ApiClient(client, builder);
            ApiException ex = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await apiClient.GetResponseStreamAsync(null, null);
            });
            Assert.Equal(400, ex.StatusCode);
            Assert.Equal("HTTP 400 : Bad Request", ex.Message);
        }
    }
}
