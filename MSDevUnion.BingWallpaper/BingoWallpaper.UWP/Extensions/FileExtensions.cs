using System.IO;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class FileExtensions
    {
        private static TaskCompletionSource<object> _lockTcs;

        public static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            while (_lockTcs != null)
            {
                await _lockTcs.Task;
            }

            _lockTcs = new TaskCompletionSource<object>();
            var currentLockTcs = _lockTcs;
            await Task.Run(() =>
            {
                var directory = Path.GetDirectoryName(path);
                Directory.CreateDirectory(directory);
                File.WriteAllBytes(path, bytes);
            });
            _lockTcs = null;
            currentLockTcs.SetResult(null);
        }
    }
}