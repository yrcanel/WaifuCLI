using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.UrlBuilder
{
    class UrlBuilder : IUrlBuilder
    {
        public string BuildUrlWithTags(string[]? tags, bool? isNsfw)
        {
            string url = string.Empty;
            if (tags != null)
            {
                foreach (string tag in tags)
                {
                    url += $"&IncludedTags={tag}";
                }
            }
            
            if (isNsfw != null)
            {

                url += $"&IsNsfw={isNsfw}";
            }
            else
            {
                url += $"&IsNsfw=All";
            }
            
            return url ;
            
        }
    }
}
