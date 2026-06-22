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
                    },
                    {
            			"id": 7317,
            			"isNsfw": false,
            			"url": "https://cdn.waifu.im/7317.jpg"
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
            builder.BuildUrlWithTags(Arg.Any<string[]?>(), Arg.Any<bool?>(), 2).Returns("https://api.waifu.im/images?IncludedTags=ecchi&isNsfw=True&pageSize=2");
            ApiClient apiClient = new ApiClient(client, builder);
            Stream respStream = await apiClient.GetResponseStreamAsync(["ecchi"], true, 2); 

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
            builder.BuildUrlWithTags(Arg.Any<string[]?>(), Arg.Any<bool?>(), null).Returns("https://api.waifu.im/images?Tag=ecchi&isNsfw=True&pageSize=1");
            ApiClient apiClient = new ApiClient(client, builder);
            ApiException ex = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await apiClient.GetResponseStreamAsync(null, null, null);
            });
            Assert.Equal(400, ex.StatusCode);
            Assert.Equal("HTTP 400 : Bad Request", ex.Message);
        }
        [Fact]
        public async Task GetTagStreamAsync_SuccessfulCode_ShouldReturnStream()
        {
            string json = """
            {
            	"items": 
                [
            		{
            			"id": 12,
            			"name": "Waifu",
            			"slug": "waifu",
            			"description": "A female anime/manga character.",
            			"reviewStatus": "Accepted",
            			"creatorId": null,
            			"imageCount": 4269
            		},
            		{
            			"id": 3,
            			"name": "Ero",
            			"slug": "ero",
            			"description": "Any kind of erotic content, basically any nsfw image.",
            			"reviewStatus": "Accepted",
            			"creatorId": null,
            			"imageCount": 3006
            		},
            		{
            			"id": 2,
            			"name": "Ecchi",
            			"slug": "ecchi",
            			"description": "Slightly explicit sexual content. Show full to partial nudity. Doesn't show any genital.",
            			"reviewStatus": "Accepted",
            			"creatorId": null,
            			"imageCount": 2129
            		},
            		
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
            builder.BuildUrlForTags().Returns("https://api.waifu.im/tags?");
            ApiClient apiClient = new ApiClient(client, builder);
            Stream respStream = await apiClient.GetTagsStreamAsync();

            Assert.NotNull(respStream);
        }

        [Fact]
        public async Task GetTagStreamAsync_UnsuccessfulCode_ShouldThrow()
        {

            HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                ReasonPhrase = "Not Found"
            };
            HttpMessageHandler handler = new FakeHandler(httpResponse);
            HttpClient client = new HttpClient(handler);
            IUrlBuilder builder = Substitute.For<IUrlBuilder>();
            builder.BuildUrlForTags().Returns("https://api.waifu.im/taks?");
            ApiClient apiClient = new ApiClient(client, builder);
            ApiException ex = await Assert.ThrowsAsync<ApiException>(async () =>
            {
                await apiClient.GetTagsStreamAsync();
            });
            Assert.Equal(404, ex.StatusCode);
            Assert.Equal("HTTP 404 : Not Found", ex.Message);
        }
    }
}
