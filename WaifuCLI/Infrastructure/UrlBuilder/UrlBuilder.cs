using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.UrlBuilder
{
    public class UrlBuilder : IUrlBuilder
    {
        private UriBuilder _uriBuilder;
        public UrlBuilder(UriBuilder uriBuilder)
        {
            _uriBuilder = uriBuilder;
        }

        public string BuildUrlWithTags(string[]? tags, bool? isNsfw, int? ammount)
        {
            _uriBuilder.Path = "images";
            if (tags != null)
            {
                foreach (string tag in tags)
                {
                    AppendValue($"IncludedTags={tag}");
                }
            }
            
            if (isNsfw != null)
            {
                AppendValue($"IsNsfw={isNsfw}");
            }
            else
            {
                AppendValue($"IsNsfw=All");
            }
            if (ammount != null)
            {
                AppendValue($"pageSize={ammount}");
            }
            else
            {
                AppendValue($"pageSize=1");
            }
            
            return _uriBuilder.Uri.AbsoluteUri;
            
            
        }
        public string BuildUrlForTags()
        {
            _uriBuilder.Query = "";
            _uriBuilder.Path = "tags";
            return _uriBuilder.Uri.AbsoluteUri;
        }
        private void AppendValue(string value) 
        {
            if (_uriBuilder.Query != null && _uriBuilder.Query.Length > 1)
            {
                _uriBuilder.Query = _uriBuilder.Query + "&" + value;
            }
            else
            {
                _uriBuilder.Query += value;
            }
        }
    }
}
