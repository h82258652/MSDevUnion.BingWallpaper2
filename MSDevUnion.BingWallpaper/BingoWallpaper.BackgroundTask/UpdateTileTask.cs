using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using System;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IWallpaperService _wallpaperService;

        private readonly IBingoWallpaperSettings _settings;

        public UpdateTileTask()
        {
            _wallpaperService = new WallpaperService();
            _settings = new BingoWallpaperSettings();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var wallpaper = await _wallpaperService.GetNewestWallpaperAsync(_settings.SelectedArea);
                throw new NotImplementedException();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}