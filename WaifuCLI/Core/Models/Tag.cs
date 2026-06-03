using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Models
{
    public record class Tag(int id, string name, string slug, string description);
}
