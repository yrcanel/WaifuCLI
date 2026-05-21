using System;
using System.Collections.Generic;
using System.Text;

namespace WaifuCLI.Core.Interfaces
{
    interface ICLI
    {
        Task<int> StartCli(string[] args);
    }
}
