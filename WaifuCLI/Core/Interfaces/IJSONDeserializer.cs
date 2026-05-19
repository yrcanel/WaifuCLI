using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Models;


namespace WaifuCLI.Core.Interfaces
{
    interface IJSONDeserializer
    {
        Task<WaifuImage> DeserializeJsonAsync(Stream responseStream);
    }
}
