using BingoWallpaper.Models;
using Microsoft.EntityFrameworkCore;

namespace BingoWallpaper.Uwp.Repositories
{
    public class WallpaperContext : DbContext
    {
        public DbSet<Archive> Archives
        {
            get;
            set;
        }

        public DbSet<Image> Images
        {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite($"Filename={Constants.DatabaseName}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Archive>().HasKey(temp => temp.ObjectId);
            modelBuilder.Entity<Image>().HasKey(temp => temp.ObjectId);

            modelBuilder.Entity<CoverStory>().HasKey(temp => temp.Id);
            modelBuilder.Entity<CoverStory>().Property(temp => temp.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Hotspot>().HasKey(temp => temp.Id);
            modelBuilder.Entity<Hotspot>().Property(temp => temp.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<LeanCloudPointer>().HasKey(temp => temp.ObjectId);

            modelBuilder.Entity<Message>().HasKey(temp => temp.Id);
        }
    }
}