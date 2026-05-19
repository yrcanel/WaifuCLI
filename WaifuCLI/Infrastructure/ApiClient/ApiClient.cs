using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Exceptions;
namespace WaifuCLI.Infrastructure.ApiClient
{
    class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private readonly IURLBuilder URLBuilder;
        public ApiClient(HttpClient httpClient, IURLBuilder URLBuilder) 
        {
            this.httpClient = httpClient;
            this.URLBuilder = URLBuilder;
        }
        public async Task<Stream> GetResponseStreamAsync(string[]? tags, bool? isNsfw)
        {
            string requestURL = URLBuilder.BuildUrlWithTags(tags, isNsfw);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(httpClient.BaseAddress + requestURL);
            
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
    }
}
