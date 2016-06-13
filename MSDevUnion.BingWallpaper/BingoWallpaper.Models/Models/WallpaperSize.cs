using System;

namespace BingoWallpaper.Models
{
    public struct WallpaperSize : IEquatable<WallpaperSize>
    {
        public WallpaperSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Height
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

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
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}