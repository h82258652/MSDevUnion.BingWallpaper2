using System;
using System.Collections.Generic;

namespace BingoWallpaper.Models
{
    public struct WallpaperSize : IEquatable<WallpaperSize>
    {
        public int Height;

        public int Width;

        public WallpaperSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static IReadOnlyList<WallpaperSize> SupportSizes => new[]
        {
            new WallpaperSize(480,800),
            new WallpaperSize(768,1280),
            new WallpaperSize(800,480),
            new WallpaperSize(1080,1920),
            new WallpaperSize(1366,768),
            new WallpaperSize(1920,1080),
            new WallpaperSize(1920,1200),
        };

        public override bool Equals(object obj)
        {
            if (obj is WallpaperSize == false)
            {
                return false;
            }
            return Equals((WallpaperSize)obj);
        }

        public bool Equals(WallpaperSize other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}