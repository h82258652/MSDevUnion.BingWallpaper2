using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using WinRTXamlToolkit.Controls.Extensions;

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
            var scrollViewer = d.GetFirstDescendantOfType<ScrollViewer>();
            var horizontalScrollBar = scrollViewer?.GetDescendantsOfType<ScrollBar>().FirstOrDefault(temp => temp.Orientation == Orientation.Horizontal);
            if (horizontalScrollBar != null)
            {
                var value = (Style)e.NewValue;
                horizontalScrollBar.Style = value;
            }
        }

        private static void VerticalScrollBarStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d.GetFirstDescendantOfType<ScrollViewer>();
            var verticalScrollBar = scrollViewer?.GetDescendantsOfType<ScrollBar>().FirstOrDefault(temp => temp.Orientation == Orientation.Vertical);
            if (verticalScrollBar != null)
            {
                var value = (Style)e.NewValue;
                verticalScrollBar.Style = value;
            }
        }
    }
}