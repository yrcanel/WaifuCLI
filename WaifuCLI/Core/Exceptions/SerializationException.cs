namespace WaifuCLI.Core.Exceptions
{
    class SerializationException : CliException
    {
        public SerializationException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
