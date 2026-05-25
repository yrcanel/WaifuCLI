namespace WaifuCLI.Core.Interfaces
{
    interface IImageClient
    {
        Task<Stream> GetImageStreamAsync(Uri url);
    }
}
