using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface IURLBuilder
    {
        string BuldUrlWithTags(string[]? tags, bool? isNsfw);
    }
}
