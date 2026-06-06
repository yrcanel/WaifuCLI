using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Models
{
    public record class Tag(int id, string name, string slug, string description)
    {
        public override string ToString()
        {
            return $"Name: {name}, Corresponding argument: {slug}, description: {description}";
        }
    }
}
