using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using System.Text.Json;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Infrastructure.JSONDeserializer
{
    class JSONDeserializer : IJSONDeserializer
    {
        public async Task<WaifuImage?> DeserializeJsonAsync(Stream responseStream)
        {
            WaifuImage? image;
            RootJSON? images = await JsonSerializer.DeserializeAsync<RootJSON>(responseStream);
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
