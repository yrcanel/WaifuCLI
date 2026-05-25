using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using System.Text.Json;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Infrastructure.JsonDeserializer
{
    class JsonDeserializer : IJsonDeserializer
    {
        public async Task<WaifuImage?> DeserializeJsonAsync(Stream responseStream)
        {
            WaifuImage? image;
            RootJson? images = await JsonSerializer.DeserializeAsync<RootJson>(responseStream);
            if (images is null)
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
    }
}
