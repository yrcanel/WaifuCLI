using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface IApiClient
    {
        Task<Stream> GetResponseStreamAsync(string[]? tags, bool? isNsfw);
        
    }
}
