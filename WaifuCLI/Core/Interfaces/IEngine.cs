namespace WaifuCLI.Core.Interfaces
{
    interface IEngine
    {
        Task GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir);
        

    }
}
