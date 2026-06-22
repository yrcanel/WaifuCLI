using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task DeserializeImageJsonAsync_GoodJson_ShouldParse()
        {
            WaifuImage[] expectedWaifuImages =
                [
                    new WaifuImage(7783, false, new Uri("https://cdn.waifu.im/7783.jpg")),
                    new WaifuImage(7317, false, new Uri("https://cdn.waifu.im/7317.jpg"))
                ];
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
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            WaifuImage[] resultWaifuImages = await jsonDeserializer.DeserializeImageJsonAsync(stream);
            foreach(WaifuImage image in resultWaifuImages)
            {
                Assert.NotNull(image);
            }
            Assert.Equal(expectedWaifuImages, resultWaifuImages);
        }
        [Fact]
        public async Task DeserializeImageJsonAsync_NoPayloadFound_ShouldThrow()
        {
            
            string json = """
            {  }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await jsonDeserializer.DeserializeImageJsonAsync(stream);
            });
        }
        [Fact]
        public async Task DeserializeImageJsonAsync_NoImagesFound_ShouldThrow()
        {

            string json = """
            { "items": [] }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            await Assert.ThrowsAsync<ImageNotFoundException>(async () =>
            {
                await jsonDeserializer.DeserializeImageJsonAsync(stream);
            });
        }
        [Fact]
        public async Task DeserializeTagJsonAsync_GoodJson_ShouldParse()
        {
            Tag expectedTag = new Tag(12, "Waifu", "waifu", "A female anime/manga character.");
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
            		}
            	]
            	
            }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            Tag[]? resultTags = await jsonDeserializer.DeserializeTagJsonAsync(stream);
            Assert.NotNull(resultTags);
            Assert.Equal(expectedTag, resultTags[0]);
        }
        [Fact]
        public async Task DeserializeTagJsonAsync_NoPayloadFound_ShouldThrow()
        {

            string json = """
            {  }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await jsonDeserializer.DeserializeTagJsonAsync(stream);
            });
        }
        [Fact]
        public async Task DeserializeTagJsonAsync_NoTagsFound_ShouldThrow()
        {

            string json = """
            { "items": [] }
            """;
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            JsonDeserializer jsonDeserializer = new JsonDeserializer();
            await Assert.ThrowsAsync<SerializationException>(async () =>
            {
                await jsonDeserializer.DeserializeTagJsonAsync(stream);
            });
        }
    }
}
