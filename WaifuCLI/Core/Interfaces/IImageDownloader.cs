namespace WaifuCLI.Core.Interfaces
{
    interface IImageDownloader
    {
        Task DownloadImageAsync(string imagePath, Stream ImageStream);
    }
}
