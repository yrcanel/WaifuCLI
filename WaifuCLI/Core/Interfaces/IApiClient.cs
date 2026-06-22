namespace WaifuCLI.Core.Interfaces
{
    public interface IApiClient
    {
        Task<Stream> GetResponseStreamAsync(string[]? tags, bool? isNsfw, int? amount);
        Task<Stream> GetTagsStreamAsync();
    }
}
