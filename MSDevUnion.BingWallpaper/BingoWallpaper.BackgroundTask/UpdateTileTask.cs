using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IBingoWallpaperSettings _settings;

        private readonly ITileService _tileService;

        private readonly ILeanCloudWallpaperService _leanCloudWallpaperService;

        public UpdateTileTask()
        {
            _leanCloudWallpaperService = new LeanCloudWallpaperService();
            _settings = new BingoWallpaperSettings(_leanCloudWallpaperService);
            _tileService = new TileService(_leanCloudWallpaperService);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var wallpaper = await _leanCloudWallpaperService.GetNewestWallpaperAsync(_settings.SelectedArea);
                _tileService.UpdatePrimaryTile(wallpaper);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}