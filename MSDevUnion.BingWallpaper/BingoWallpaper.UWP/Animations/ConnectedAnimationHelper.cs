using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace BingoWallpaper.Uwp.Animations
{
    internal static class ConnectedAnimationHelper
    {
        private static Canvas _animationContainer;

        internal static Panel GetAnimationContainer()
        {
            if (_animationContainer == null)
            {
                var currentWindow = Window.Current;
                if (currentWindow == null)
                {
                    return null;
                }

                var currentWindowSize = currentWindow.Bounds;
                _animationContainer = new Canvas
                {
                    Width = currentWindowSize.Width,
                    Height = currentWindowSize.Height
                };
                currentWindow.SizeChanged += (sender, e) =>
                {
                    var size = e.Size;
                    _animationContainer.Width = size.Width;
                    _animationContainer.Height = size.Height;
                };

                var popup = new Popup
                {
                    Child = _animationContainer,
                    IsOpen = true
                };
            }
            return _animationContainer;
        }

        internal static UIElement AddToAnimationLayer(RenderTargetBitmap elementScreenShot)
        {
            if (elementScreenShot == null)
            {
                throw new ArgumentNullException(nameof(elementScreenShot));
            }

            if (GetAnimationContainer() != null)
            {
                var transformGroup = new TransformGroup();
                transformGroup.Children.Add(new ScaleTransform());
                transformGroup.Children.Add(new TranslateTransform());

                var image = new Image()
                {
                    Source = elementScreenShot,
                    RenderTransform = transformGroup
                };

                GetAnimationContainer().Children.Add(image);
                return image;
            }
            else
            {
                return null;
            }
        }

        internal static void RemoveFromAnimationLayer(RenderTargetBitmap elementScreenShot)
        {
            if (elementScreenShot == null)
            {
                throw new ArgumentNullException(nameof(elementScreenShot));
            }

            var image = GetAnimationContainer()?.Children.OfType<Image>().FirstOrDefault(temp => ReferenceEquals(temp.Source, elementScreenShot));
            if (image != null)
            {
                image.Source = null;
                GetAnimationContainer().Children.Remove(image);
            }
        }
    }
}