using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface IImageClient
    {
        Task<Stream> GetImageStreamAsync(Uri url);
    }
}
