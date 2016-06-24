using System;
using System.IO;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class FileExtensions
    {
        public static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            // TODO
            await Task.Run(() =>
            {
                var directory = Path.GetDirectoryName(path);
                Directory.CreateDirectory(directory);
                try
                {
                    File.WriteAllBytes(path, bytes);
                }
                catch (IOException)
                {
                }
            });
        }
    }
}