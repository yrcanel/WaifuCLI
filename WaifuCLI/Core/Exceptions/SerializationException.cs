using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Exceptions
{
    class SerializationException : CliException
    {
        public SerializationException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
