using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BingoWallpaper.Uwp.Models;

namespace BingoWallpaper.Uwp.Migrations
{
    [DbContext(typeof(WallpaperContext))]
    [Migration("20160613085704_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("BingoWallpaper.Uwp.Models.ArchiveRepository", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Area");

                    b.Property<string>("Date");

                    b.Property<string>("Json");

                    b.HasKey("Id");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("BingoWallpaper.Uwp.Models.ImageRepository", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Json");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });
        }
    }
}
