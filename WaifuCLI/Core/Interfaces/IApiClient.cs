namespace WaifuCLI.Core.Interfaces
{
    public interface IApiClient
    {
        Task<Stream> GetResponseStreamAsync(string[]? tags, bool? isNsfw, int? ammount);
        Task<Stream> GetTagsStreamAsync();
    }
}
