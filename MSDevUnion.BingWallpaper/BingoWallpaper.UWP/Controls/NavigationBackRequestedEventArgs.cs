﻿using System;

namespace BingoWallpaper.UWP.Controls
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