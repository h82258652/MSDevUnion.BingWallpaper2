﻿using BingoWallpaper.Uwp.Animations;
using BingoWallpaper.Uwp.ViewModels;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BingoWallpaper.Uwp.Views
{
    public sealed partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
        }

        public DetailViewModel ViewModel => (DetailViewModel)DataContext;

        protected override void OnNavigationBackRequested(NavigationBackRequestedEventArgs e)
        {
            base.OnNavigationBackRequested(e);

            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }

        protected override void OnXButton1Released(PointerRoutedEventArgs e)
        {
            base.OnXButton1Released(e);

            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
    }
}