using System;

namespace BingoWallpaper.Uwp.Controls
{
    public sealed class NavigationBackRequestedEventArgs : EventArgs
    {
        internal NavigationBackRequestedEventArgs()
        {
        }

        public bool Handled
        {
            get;
            set;
        }
    }
}