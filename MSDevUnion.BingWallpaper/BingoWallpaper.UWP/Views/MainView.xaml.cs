using BingoWallpaper.Uwp.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            _timer.Tick += Timer_Tick;
        }

        public MainViewModel ViewModel => (MainViewModel)DataContext;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.IsBusy = ViewModel.IsBusy == false;
        }

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

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
        }
    }
}