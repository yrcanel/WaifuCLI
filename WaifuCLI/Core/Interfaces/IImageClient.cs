namespace WaifuCLI.Core.Interfaces
{
    public interface IImageClient
    {
        Task<Stream> GetImageStreamAsync(Uri url);
    }
}
