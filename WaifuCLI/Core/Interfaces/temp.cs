using WaifuCLI.Core.Models;


namespace WaifuCLI.Core.Interfaces
{
    interface IJsonDeserializer
    {
        Task<WaifuImage?> DeserializeJsonAsync(Stream responseStream);
    }
}
