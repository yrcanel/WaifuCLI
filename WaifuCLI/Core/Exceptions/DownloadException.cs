using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Exceptions
{
    class DownloadException : CliException
    {
        public DownloadException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
