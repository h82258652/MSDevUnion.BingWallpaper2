using System;
using Windows.UI.Xaml;

namespace BingoWallpaper.Uwp.Helpers
{
    public static class ScrollViewerHelper
    {
        public static readonly DependencyProperty HorizontalScrollBarStyleProperty = DependencyProperty.RegisterAttached("HorizontalScrollBarStyle", typeof(Style), typeof(ScrollViewerHelper), new PropertyMetadata(null, HorizontalScrollBarStyleChanged));

        public static readonly DependencyProperty VerticalScrollBarStyleProperty = DependencyProperty.RegisterAttached("VerticalScrollBarStyle", typeof(Style), typeof(ScrollViewerHelper), new PropertyMetadata(null, VerticalScrollBarStyleChanged));

        public static Style GetHorizontalScrollBarStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(HorizontalScrollBarStyleProperty);
        }

        public static Style GetVerticalScrollBarStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(VerticalScrollBarStyleProperty);
        }

        public static void SetHorizontalScrollBarStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(HorizontalScrollBarStyleProperty, value);
        }

        public static void SetVerticalScrollBarStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(VerticalScrollBarStyleProperty, value);
        }

        private static void HorizontalScrollBarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void VerticalScrollBarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}