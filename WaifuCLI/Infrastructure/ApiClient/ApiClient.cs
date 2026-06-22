using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Infrastructure.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IUrlBuilder _UrlBuilder;
        public ApiClient(HttpClient httpClient, IUrlBuilder UrlBuilder) 
        {
            _httpClient = httpClient;
            _UrlBuilder = UrlBuilder;
        }
        public async Task<Stream> GetResponseStreamAsync(string[]? tags, bool? isNsfw, int? amount)
        {
            string requestURL = _UrlBuilder.BuildUrlWithTags(tags, isNsfw, amount);
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(requestURL);
            
            try 
            {
                responseMessage.EnsureSuccessStatusCode();
            }          
            catch (HttpRequestException ex)
            {
                throw new ApiException($"HTTP {(int)responseMessage.StatusCode} : {responseMessage.ReasonPhrase}", (int)responseMessage.StatusCode, ex);
            }
            Stream responceStream = await responseMessage.Content.ReadAsStreamAsync();     
            return responceStream;
        }

        public async Task<Stream> GetTagsStreamAsync()
        {
            string requestUrl = _UrlBuilder.BuildUrlForTags();
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(requestUrl);
            try
            {
                responseMessage.EnsureSuccessStatusCode();
            }
            catch(HttpRequestException ex)
            {
                throw new ApiException($"HTTP {(int)responseMessage.StatusCode} : {responseMessage.ReasonPhrase}", (int)responseMessage.StatusCode, ex);
            }
            Stream responseStream = await responseMessage.Content.ReadAsStreamAsync();
            return responseStream;
        }
    }
}
