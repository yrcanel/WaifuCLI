using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Models
{
    record class WaifuImage(int id, bool isNsfw, Uri url);
    
}
