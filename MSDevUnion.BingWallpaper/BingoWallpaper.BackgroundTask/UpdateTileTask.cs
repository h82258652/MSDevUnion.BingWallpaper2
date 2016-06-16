using BingoWallpaper.Configuration;
using BingoWallpaper.Services;
using System;
using System.Linq;
using Windows.ApplicationModel.Background;

namespace BingoWallpaper.BackgroundTask
{
    public sealed class UpdateTileTask : IBackgroundTask
    {
        private readonly IBingWallpaperService _bingWallpaperService;

        private readonly IBingoWallpaperSettings _settings;

        private readonly ITileService _tileService;

        private readonly IScreenService _screenService;

        public UpdateTileTask()
        {
            _bingWallpaperService = new BingWallpaperService();
            _settings = new BingoWallpaperSettings(_bingWallpaperService);
            _tileService = new TileService(_bingWallpaperService);
            _screenService = new ScreenService();
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            try
            {
                var result = await _bingWallpaperService.GetAsync(0, 1, _settings.SelectedArea);
                var image = result?.Images.FirstOrDefault();
                if (image != null)
                {
                    var copyright = image.Copyright;
                    var index = copyright.LastIndexOf("(©", StringComparison.Ordinal);
                    var text = index <= -1 ? copyright : copyright.Substring(0, index).Trim();
                    _tileService.UpdatePrimaryTile(image, text);

                    if (_settings.AutoUpdateWallpaper || _settings.AutoUpdateLockScreen)
                    {
                        var width = await _screenService.GetWidthAsync();
                        var height = await _screenService.GetHeightAsync();

                        // 判断当前 Service 是否包含此尺寸。
                        // TODO

                        if (_settings.AutoUpdateWallpaper)
                        {
                            // TODO
                        }

                        if (_settings.AutoUpdateLockScreen)
                        {
                            // TODO
                        }
                    }
                }
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}