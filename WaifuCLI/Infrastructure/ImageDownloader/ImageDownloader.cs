using WaifuCLI.Core.Interfaces;
using WaifuCLI.Core.Exceptions;

namespace WaifuCLI.Infrastructure.ImageDownloader
{
    public class ImageDownloader : IImageDownloader
    {
        public async Task DownloadImageAsync(string imagePath, Stream ImageStream)
        {

            try
            {
                await using FileStream output = File.Create(imagePath);
                await ImageStream.CopyToAsync(output);
            }
            catch(UnauthorizedAccessException)
            {
                throw new DownloadException("Failed to write file: UnauthorizedAccess");
            }
            catch (DirectoryNotFoundException)
            {
                throw new DownloadException("Specified directory isn't found");
            }
            catch (PathTooLongException)
            {
                throw new DownloadException($"Specified path is too long");
            }
            
            
        }
    }
}
