using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using WaifuCLI.Core.Interfaces;

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
            string requestURL = URLBuilder.BuldUrlWithTags(tags, isNsfw);
            HttpResponseMessage responseMessage = await httpClient.GetAsync(httpClient.BaseAddress + requestURL);           
            responseMessage.EnsureSuccessStatusCode();           
            Stream responceStream = await responseMessage.Content.ReadAsStreamAsync();     
            return responceStream;
        }
    }
}
