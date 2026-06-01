namespace WaifuCLI.Core.Interfaces
{
    public interface IImageDownloader
    {
        Task DownloadImageAsync(string imagePath, Stream ImageStream);
    }
}
