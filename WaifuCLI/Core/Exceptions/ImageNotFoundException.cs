using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Exceptions
{
    public class ImageNotFoundException : CliException
    {
        public ImageNotFoundException(string message, Exception? inner = null) : base(message, inner)
        {
        }
    }
}
