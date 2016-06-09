using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

namespace BingoWallpaper.Uwp.Animations
{
    public sealed class ConnectedAnimationService
    {
        internal readonly Dictionary<string, ConnectedAnimation> Animations = new Dictionary<string, ConnectedAnimation>();

        private static ConnectedAnimationService _currentView;

        private ConnectedAnimationService()
        {
            DefaultDuration = TimeSpan.FromSeconds(0.3);
        }

        public TimeSpan DefaultDuration
        {
            get;
            set;
        }

        public EasingFunctionBase DefaultEasingFunction
        {
            get;
            set;
        }

        public static ConnectedAnimationService GetForCurrentView()
        {
            if (_currentView == null)
            {
                _currentView = new ConnectedAnimationService();
            }
            return _currentView;
        }

        public ConnectedAnimation GetAnimation(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            ConnectedAnimation animation;
            Animations.TryGetValue(key, out animation);
            return animation;
        }

        public async Task<ConnectedAnimation> PrepareToAnimateAsync(string key, UIElement source)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(source);

            var animation = new ConnectedAnimation(this, key, source, bitmap);
            Animations[key] = animation;
            return animation;
        }
    }
}