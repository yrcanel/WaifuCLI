namespace WaifuCLI.Core.Interfaces
{
    public interface IUrlBuilder
    {
        string BuildUrlWithTags(string[]? tags, bool? isNsfw, int? ammount);
        string BuildUrlForTags();
    }
}
