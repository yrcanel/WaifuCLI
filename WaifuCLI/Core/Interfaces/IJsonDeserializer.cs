using WaifuCLI.Core.Models;


namespace WaifuCLI.Core.Interfaces
{
    public interface IJsonDeserializer
    {
        Task<WaifuImage?> DeserializeJsonAsync(Stream responseStream);
    }
}
