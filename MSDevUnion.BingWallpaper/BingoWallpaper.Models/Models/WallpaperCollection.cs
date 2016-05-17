using System.Collections.ObjectModel;

namespace BingoWallpaper.Models
{
    public class WallpaperCollection : ObservableCollection<Wallpaper>
    {
        private readonly int _year;

        private readonly int _month;

        public int Year
        {
            get
            {
                return _year;
            }
        }

        public int Month
        {
            get
            {
                return _month;
            }
        }

        public WallpaperCollection(int year, int month)
        {
            _year = year;
            _month = month;
        }
    }
}