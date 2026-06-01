namespace WaifuCLI.Core.Exceptions
{
    public class DownloadException : CliException
    {
        public DownloadException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
