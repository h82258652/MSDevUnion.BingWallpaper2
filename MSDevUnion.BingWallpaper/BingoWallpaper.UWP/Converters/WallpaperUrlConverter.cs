using BingoWallpaper.Models;
using BingoWallpaper.Services;
using Microsoft.Practices.ServiceLocation;
using System;
using Windows.UI.Xaml.Data;

namespace BingoWallpaper.Uwp.Converters
{
    public class WallpaperUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var image = value as Image;
            var wallpaper = value as Wallpaper;
            if (wallpaper != null)
            {
                image = wallpaper.Image;
            }
            var args = (parameter as string)?.Split('x', ',');
            if (image != null && args != null && args.Length == 2)
            {
                var wallpaperService = ServiceLocator.Current.GetInstance<IWallpaperService>();
                return wallpaperService.GetUrl(image, new WallpaperSize(int.Parse(args[0]), int.Parse(args[1])));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}