using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Models;

namespace WaifuCLI.Utils
{
    static class Utils
    {
        public static async Task PrintTagsAsync(Tag[] tags)
        {
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag.ToString());
            }
        } 
    }
}
