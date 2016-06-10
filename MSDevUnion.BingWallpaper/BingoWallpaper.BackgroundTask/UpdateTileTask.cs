using BingoWallpaper.Configuration;
using BingoWallpaper.Models;
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
            _settings = new BingoWallpaperSettings(_wallpaperService);
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var wallpaper = await _wallpaperService.GetNewestWallpaperAsync(_settings.SelectedArea);

                _wallpaperService.GetUrl(wallpaper.Image, new WallpaperSize(150, 150));
                _wallpaperService.GetUrl(wallpaper.Image, new WallpaperSize(310, 150));

                throw new NotImplementedException();
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}