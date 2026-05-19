using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Exceptions
{
    abstract class CliException : Exception
    {
        protected CliException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
