using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Models;
using System.Text.Json;

namespace WaifuCLI.Infrastructure.JSONDeserializer
{
    class JSONDeserializer : IJSONDeserializer
    {
        public async Task<WaifuImage> DeserializeJsonAsync(Stream responseStream)
        {
            RootJSON? images = await JsonSerializer.DeserializeAsync<RootJSON>(responseStream);
            if (images == null)
            {
                throw new Exception("An error occured while deserializing JSON");
            }
            if (images.items.Count <= 0)
            {
                throw new Exception("No images with specified tags were found");
            }
            WaifuImage image = images.items[0]; 
            return image;
        }
    }
}
