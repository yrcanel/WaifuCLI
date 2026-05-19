using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;

namespace WaifuCLI.Infrastructure.URLBuilder
{
    class URLBuilder : IURLBuilder
    {
        public string BuldUrlWithTags(string[]? tags, bool? isNsfw)
        {
            string url = string.Empty;
            if (tags != null)
            {
                foreach (string tag in tags)
                {
                    url += $"&IncludedTags={tag}";
                }
            }
            
            if (isNsfw != null)
            {

                url += $"&IsNsfw={isNsfw}";
            }
            
            return url ;
            
        }
    }
}
