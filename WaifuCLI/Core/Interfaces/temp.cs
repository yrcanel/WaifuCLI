namespace WaifuCLI.Core.Interfaces
{
    interface IUrlBuilder
    {
        string BuildUrlWithTags(string[]? tags, bool? isNsfw);
    }
}
