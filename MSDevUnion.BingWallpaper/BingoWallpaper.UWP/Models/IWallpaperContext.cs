using Microsoft.EntityFrameworkCore;

namespace BingoWallpaper.Uwp.Models
{
    public interface IWallpaperContext
    {
        DbSet<ArchiveRepository> Archives
        {
            get;
            set;
        }

        DbSet<ImageRepository> Images
        {
            get;
            set;
        }
    }
}