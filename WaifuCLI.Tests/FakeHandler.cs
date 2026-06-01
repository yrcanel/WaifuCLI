using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Tests
{
    class FakeHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;
        public FakeHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }
}
