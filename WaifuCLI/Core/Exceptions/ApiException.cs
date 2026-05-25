namespace WaifuCLI.Core.Exceptions
{
    class ApiException : CliException
    {
        public int StatusCode { get; }
        public ApiException(string message, int statusCode, Exception? inner = null) : base(message, inner) 
        {
            StatusCode = statusCode;
        }
    }
}
