using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using WinRTXamlToolkit.AwaitableUI;

namespace BingoWallpaper.Uwp.Animations
{
    public sealed class ConnectedAnimation
    {
        private readonly RenderTargetBitmap _elementScreenShot;

        private readonly string _key;

        private readonly ConnectedAnimationService _ownerConnectedAnimationService;

        private readonly Point _sourcePosition;

        private readonly Size _sourceSize;

        internal ConnectedAnimation(ConnectedAnimationService ownerConnectedAnimationService, string key, UIElement source, RenderTargetBitmap elementScreenShot)
        {
            _ownerConnectedAnimationService = ownerConnectedAnimationService;

            _key = key;

            _sourcePosition = source.TransformToVisual(ConnectedAnimationHelper.GetAnimationContainer()).TransformPoint(new Point());

            _sourceSize = source.RenderSize;

            _elementScreenShot = elementScreenShot;
        }

        public event TypedEventHandler<ConnectedAnimation, EventArgs> Completed;

        public void Cancel()
        {
            ConnectedAnimationHelper.RemoveFromAnimationLayer(_elementScreenShot);
            _ownerConnectedAnimationService.Animations.Remove(_key);
        }

        public bool TryStart(UIElement destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var animator = ConnectedAnimationHelper.AddToAnimationLayer(_elementScreenShot);
            if (animator == null)
            {
                Cancel();
                return false;
            }

            TryStartContinue(destination, animator);
            return true;
        }

        private async void TryStartContinue(UIElement destination, UIElement animator)
        {
            var element = destination as FrameworkElement;
            if (element != null)
            {
                await element.WaitForLoadedAsync();
            }

            var destinationPosition = destination.TransformToVisual(ConnectedAnimationHelper.GetAnimationContainer()).TransformPoint(new Point());
            var destinationSize = destination.RenderSize;

            var storyboard = new Storyboard();
            {
                var animation = new DoubleAnimation
                {
                    From = _sourcePosition.X,
                    To = destinationPosition.X,
                    Duration = _ownerConnectedAnimationService.DefaultDuration,
                    EasingFunction = _ownerConnectedAnimationService.DefaultEasingFunction
                };
                Storyboard.SetTarget(animation, animator);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation
                {
                    From = _sourcePosition.Y,
                    To = destinationPosition.Y,
                    Duration = _ownerConnectedAnimationService.DefaultDuration,
                    EasingFunction = _ownerConnectedAnimationService.DefaultEasingFunction
                };
                Storyboard.SetTarget(animation, animator);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.Y)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation
                {
                    From = 1,
                    To = destinationSize.Width / _sourceSize.Width,
                    Duration = _ownerConnectedAnimationService.DefaultDuration,
                    EasingFunction = _ownerConnectedAnimationService.DefaultEasingFunction
                };
                Storyboard.SetTarget(animation, animator);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");
                storyboard.Children.Add(animation);
            }
            {
                var animation = new DoubleAnimation
                {
                    From = 1,
                    To = destinationSize.Height / _sourceSize.Height,
                    Duration = _ownerConnectedAnimationService.DefaultDuration,
                    EasingFunction = _ownerConnectedAnimationService.DefaultEasingFunction
                };
                Storyboard.SetTarget(animation, animator);
                Storyboard.SetTargetProperty(animation, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                storyboard.Children.Add(animation);
            }
            EventHandler<object> completedHandler = null;
            completedHandler += (sender, e) =>
            {
                storyboard.Completed -= completedHandler;
                Cancel();
                Completed?.Invoke(this, EventArgs.Empty);
            };
            storyboard.Completed += completedHandler;
            storyboard.Begin();
        }
    }
}