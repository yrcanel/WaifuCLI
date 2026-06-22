namespace WaifuCLI.Core.Interfaces
{
    public interface IEngine
    {
        Task GetAndDownloadImageAsync(string[]? tags, bool? IsNsfw, string outputDir, int? ammount);
        Task GetAndPrintTagsAsync();



    }
}
