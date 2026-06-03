using System.Text.Json;
using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WaifuCLI.Infrastructure.JsonDeserializer
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public async Task<WaifuImage?> DeserializeImageJsonAsync(Stream responseStream)
        {
            WaifuImage? image;
            RootJson<WaifuImage>? images = await JsonSerializer.DeserializeAsync<RootJson<WaifuImage>>(responseStream);
            if (images is null || images.items is null)
            {
                throw new SerializationException("API returned null payload");
            }
            if (images.items.Count <= 0)
            {
                image = null;
                return image;
            }
            image = images.items[0]; 
            return image;
        }
        public async Task<Tag[]> DeserializeTagJsonAsync(Stream responseStream)
        {

            RootJson<Tag>? tags = await JsonSerializer.DeserializeAsync<RootJson<Tag>>(responseStream);
            if (tags is null || tags.items is null)
            {
                throw new SerializationException("API returned null payload");
            }
            if (tags.items.Count <= 0)
            {
                throw new SerializationException("API returned no tags");
            }
            return tags.items.ToArray();
        }
    }
}
