using Microsoft.Xaml.Interactivity;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Behaviors
{
    public class BingoWallpaperAppBarBehavior : Behavior<AppBar>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SizeChanged += AppBar_SizeChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SizeChanged -= AppBar_SizeChanged;
        }

        private void AppBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // TODO
        }
    }
}