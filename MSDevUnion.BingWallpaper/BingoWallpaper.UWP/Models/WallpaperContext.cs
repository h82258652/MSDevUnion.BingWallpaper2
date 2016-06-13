using Microsoft.EntityFrameworkCore;

namespace BingoWallpaper.Uwp.Models
{
    public class WallpaperContext : DbContext, IWallpaperContext
    {
        public DbSet<ArchiveRepository> Archives
        {
            get;
            set;
        }

        public DbSet<ImageRepository> Images
        {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Constants.DatabaseName}");
        }
    }
}