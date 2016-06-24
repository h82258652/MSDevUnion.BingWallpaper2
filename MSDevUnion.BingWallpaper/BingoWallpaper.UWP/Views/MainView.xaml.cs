using BingoWallpaper.Uwp.Animations;
using BingoWallpaper.Uwp.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.Extensions;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class MainView
    {
        private const double TimerInterval = 0.03;

        private readonly DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(TimerInterval)
        };

        public MainView()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            _timer.Tick += Timer_Tick;
        }

        public MainViewModel ViewModel => (MainViewModel)DataContext;

        private void Timer_Tick(object sender, object e)
        {
            var angle = RefreshIconRotateTransform.Angle;
            angle += 360d * TimerInterval;
            if (angle >= 360d)
            {
                angle -= 360d;
            }
            RefreshIconRotateTransform.Angle = angle;
        }

        private async void WallpaperItem_Click(object sender, ItemClickEventArgs e)
        {
            var view = (ItemsControl)sender;
            var wallpaperItem = view.ContainerFromItem(e.ClickedItem);
            var image = wallpaperItem.GetFirstDescendantOfType<Image>();
            if (image != null)
            {
                await ConnectedAnimationService.GetForCurrentView().PrepareToAnimateAsync("WallpaperAnimation", image);
            }
            Frame.Navigate(typeof(DetailView), e.ClickedItem);
        }
    }
}