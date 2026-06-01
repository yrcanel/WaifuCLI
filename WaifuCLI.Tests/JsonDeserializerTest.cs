using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using WaifuCLI.Infrastructure.JsonDeserializer;

namespace WaifuCLI.Tests
{
    public class JsonDeserializerTest
    {
        [Fact]
        public async Task DeserializeJsonAsync_GoodJson_ShouldParse()
        {
            WaifuImage expectedWaifuImage = new WaifuImage(7783, false, new Uri("https://cdn.waifu.im/7783.jpg"));
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
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            WaifuImage? resultWaifuImage = await jsonDeserializer.DeserializeJsonAsync(stream);
            Assert.NotNull(resultWaifuImage);
            Assert.Equal(expectedWaifuImage, resultWaifuImage);
        }
        [Fact]
        public async Task DeserializeJsonAsync_NoPayloadFound_ShouldParse()
        {
            
            string json = """
            {  }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await jsonDeserializer.DeserializeJsonAsync(stream);
            });
        }
        [Fact]
        public async Task DeserializeJsonAsync_NoImagesFound_ShouldParse()
        {

            string json = """
            { "items": [] }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            WaifuImage? image = await jsonDeserializer.DeserializeJsonAsync(stream);
            Assert.Null(image);
        }
    }
}
