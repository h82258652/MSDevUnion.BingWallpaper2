using BingoWallpaper.Services;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IWallpaperService _wallpaperService;

        public UpdateTileTask()
        {
            _wallpaperService = new WallpaperService();
        }

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                // TODO
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}