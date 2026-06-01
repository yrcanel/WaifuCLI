namespace WaifuCLI.Core.Exceptions
{
    public class SerializationException : CliException
    {
        public SerializationException(string message, Exception? inner = null) : base(message, inner) { }
    }
}
