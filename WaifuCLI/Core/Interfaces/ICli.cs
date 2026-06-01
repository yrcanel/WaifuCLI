namespace WaifuCLI.Core.Interfaces
{
    public interface ICli
    {
        Task<int> StartCli(string[] args);
    }
}
