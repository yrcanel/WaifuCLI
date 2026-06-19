using WaifuCLI.Core.Models;


namespace WaifuCLI.Core.Interfaces
{
    public interface IJsonDeserializer
    {
        Task<WaifuImage> DeserializeImageJsonAsync(Stream responseStream);
        Task<Tag[]> DeserializeTagJsonAsync(Stream responseStream);
    }
}
