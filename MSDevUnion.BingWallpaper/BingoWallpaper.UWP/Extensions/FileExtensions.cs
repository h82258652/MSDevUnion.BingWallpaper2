using System.IO;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class FileExtensions
    {
        public static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            await Task.Run(() =>
            {
                var directory = Path.GetDirectoryName(path);
                Directory.CreateDirectory(directory);
                File.WriteAllBytes(path, bytes);
            });
        }
    }
}