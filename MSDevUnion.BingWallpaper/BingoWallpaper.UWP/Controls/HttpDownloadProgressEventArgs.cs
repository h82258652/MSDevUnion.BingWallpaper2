using System;
using Windows.Web.Http;

namespace BingoWallpaper.Uwp.Controls
{
    public sealed class HttpDownloadProgressEventArgs : EventArgs
    {
        public HttpDownloadProgressEventArgs(HttpProgress progress)
        {
            Progress = progress;
        }

        public HttpProgress Progress
        {
            get;
        }
    }
}