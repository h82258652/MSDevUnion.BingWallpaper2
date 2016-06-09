using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BingoWallpaper.Utils
{
    public sealed class StringTaskLocker : IDisposable
    {
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<object>> LockTcses = new ConcurrentDictionary<string, TaskCompletionSource<object>>();

        private readonly string _stringToLock;

        private readonly TaskCompletionSource<object> _tcs;

        private StringTaskLocker(string stringToLock, TaskCompletionSource<object> tcs)
        {
            _stringToLock = stringToLock;
            _tcs = tcs;
        }

        public static async Task<StringTaskLocker> GetLockerAsync(string stringToLock)
        {
            if (stringToLock == null)
            {
                throw new ArgumentNullException(nameof(stringToLock));
            }

            TaskCompletionSource<object> tcs;
            while (true)
            {
                if (LockTcses.TryGetValue(stringToLock, out tcs))
                {
                    await tcs.Task;
                }
                else
                {
                    break;
                }
            }

            tcs = new TaskCompletionSource<object>();
            while (true)
            {
                if (LockTcses.TryAdd(stringToLock, tcs))
                {
                    break;
                }
            }

            return new StringTaskLocker(stringToLock, tcs);
        }

        public void Dispose()
        {
            _tcs.SetResult(null);
            TaskCompletionSource<object> tcs;
            if (LockTcses.TryRemove(_stringToLock, out tcs))
            {
                tcs.TrySetResult(null);
            }
        }
    }
}