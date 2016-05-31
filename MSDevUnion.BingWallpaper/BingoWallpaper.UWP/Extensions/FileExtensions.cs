using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace BingoWallpaper.Uwp.Extensions
{
    public static class FileExtensions
    {
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<object>> LockTcses = new ConcurrentDictionary<string, TaskCompletionSource<object>>();

        public static async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            TaskCompletionSource<object> tcs;
            while (true)
            {
                if (LockTcses.TryGetValue(path, out tcs))
                {
                    await tcs.Task;
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                tcs = new TaskCompletionSource<object>();
                if (LockTcses.TryAdd(path, tcs))
                {
                    break;
                }
            }

            await Task.Run(() =>
            {
                var directory = Path.GetDirectoryName(path);
                Directory.CreateDirectory(directory);
                File.WriteAllBytes(path, bytes);
            });

            tcs.SetResult(null);
            LockTcses.TryRemove(path, out tcs);
        }
    }
}