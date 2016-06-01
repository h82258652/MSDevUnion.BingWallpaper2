using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BingoWallpaper.Uwp.Repositories;

namespace BingoWallpaper.Uwp.Migrations
{
    [DbContext(typeof(WallpaperContext))]
    [Migration("20160601162849_MyFirstMigration")]
    partial class MyFirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901");

            modelBuilder.Entity("BingoWallpaper.Models.Archive", b =>
                {
                    b.Property<string>("ObjectId");

                    b.Property<Guid?>("CoverStoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Date");

                    b.Property<int>("ErrorCode");

                    b.Property<string>("ErrorMessage");

                    b.Property<string>("Info");

                    b.Property<string>("Link");

                    b.Property<string>("Market");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("ObjectId");

                    b.HasIndex("CoverStoryId");

                    b.ToTable("Archives");
                });

            modelBuilder.Entity("BingoWallpaper.Models.CoverStory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Attribute");

                    b.Property<string>("Date");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Parameter1");

                    b.Property<string>("Parameter2");

                    b.Property<string>("PrimaryImageUrl");

                    b.Property<string>("Provider");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("CoverStory");
                });

            modelBuilder.Entity("BingoWallpaper.Models.Hotspot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArchiveObjectId");

                    b.Property<string>("Description");

                    b.Property<string>("Link");

                    b.Property<int>("LocationX");

                    b.Property<int>("LocationY");

                    b.Property<string>("Query");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveObjectId");

                    b.ToTable("Hotspot");
                });

            modelBuilder.Entity("BingoWallpaper.Models.Image", b =>
                {
                    b.Property<string>("ObjectId");

                    b.Property<string>("Copyright");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("ErrorCode");

                    b.Property<string>("ErrorMessage");

                    b.Property<bool>("ExistWUXGA");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("UrlBase");

                    b.HasKey("ObjectId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BingoWallpaper.Models.LeanCloudPointer", b =>
                {
                    b.Property<string>("ObjectId");

                    b.Property<string>("ClassName");

                    b.Property<string>("Type");

                    b.HasKey("ObjectId");

                    b.HasIndex("ObjectId");

                    b.ToTable("LeanCloudPointer");
                });

            modelBuilder.Entity("BingoWallpaper.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArchiveObjectId");

                    b.Property<string>("Link");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ArchiveObjectId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("BingoWallpaper.Models.Archive", b =>
                {
                    b.HasOne("BingoWallpaper.Models.CoverStory")
                        .WithMany()
                        .HasForeignKey("CoverStoryId");
                });

            modelBuilder.Entity("BingoWallpaper.Models.Hotspot", b =>
                {
                    b.HasOne("BingoWallpaper.Models.Archive")
                        .WithMany()
                        .HasForeignKey("ArchiveObjectId");
                });

            modelBuilder.Entity("BingoWallpaper.Models.LeanCloudPointer", b =>
                {
                    b.HasOne("BingoWallpaper.Models.Archive")
                        .WithOne()
                        .HasForeignKey("BingoWallpaper.Models.LeanCloudPointer", "ObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BingoWallpaper.Models.Message", b =>
                {
                    b.HasOne("BingoWallpaper.Models.Archive")
                        .WithMany()
                        .HasForeignKey("ArchiveObjectId");
                });
        }
    }
}
