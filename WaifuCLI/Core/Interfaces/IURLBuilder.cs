using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface IURLBuilder
    {
        string BuildUrlWithTags(string[]? tags, bool? isNsfw);
    }
}
