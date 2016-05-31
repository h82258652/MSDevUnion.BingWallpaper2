﻿using BingoWallpaper.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Archive>().HasKey(temp => temp.ObjectId);
            modelBuilder.Entity<Image>().HasKey(temp => temp.ObjectId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // TODO
            optionsBuilder.UseSqlite("");
        }
    }
}