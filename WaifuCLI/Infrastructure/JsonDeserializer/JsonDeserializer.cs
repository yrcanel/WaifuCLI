using System.Text.Json;
using WaifuCLI.Core.Exceptions;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WaifuCLI.Infrastructure.JsonDeserializer
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public async Task<WaifuImage[]> DeserializeImageJsonAsync(Stream responseStream)
        {
            RootJson<WaifuImage>? images = await JsonSerializer.DeserializeAsync<RootJson<WaifuImage>>(responseStream);
            if (images is null || images.items is null)
            {
                throw new SerializationException("API returned null payload");
            }
            if (images.items.Count <= 0)
            {
                throw new ImageNotFoundException("No images with specified tags were found");
            }
            return images.items.ToArray();
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
