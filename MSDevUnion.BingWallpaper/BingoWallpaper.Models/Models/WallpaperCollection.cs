using System.Collections.ObjectModel;

namespace BingoWallpaper.Models
{
    public class WallpaperCollection : ObservableCollection<Wallpaper>
    {
        public int Year
        {
            get;
        }

        public int Month
        {
            get;
        }

        public WallpaperCollection(int year, int month)
        {
            Year = year;
            Month = month;
        }
    }
}