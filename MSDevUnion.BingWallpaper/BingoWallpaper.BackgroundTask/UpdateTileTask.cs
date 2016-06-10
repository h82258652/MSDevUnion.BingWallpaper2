using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IBingoWallpaperSettings _settings;

        private readonly ITileService _tileService;

        private readonly IWallpaperService _wallpaperService;

        public UpdateTileTask()
        {
            _wallpaperService = new WallpaperService();
            _settings = new BingoWallpaperSettings(_wallpaperService);
            _tileService = new TileService(_wallpaperService);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var wallpaper = await _wallpaperService.GetNewestWallpaperAsync(_settings.SelectedArea);
                _tileService.UpdatePrimaryTile(wallpaper);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}