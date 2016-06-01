using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    public class FirstDoubleSizePanel : Panel
    {
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(nameof(ItemHeight), typeof(double), typeof(FirstDoubleSizePanel), new PropertyMetadata(double.NaN, ItemHeightChanged));

        public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(nameof(ItemWidth), typeof(double), typeof(FirstDoubleSizePanel), new PropertyMetadata(double.NaN, ItemWidthChanged));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FirstDoubleSizePanel), new PropertyMetadata(Orientation.Horizontal, OrientationChanged));

        public static readonly DependencyProperty RowsOrColumnsProperty = DependencyProperty.Register(nameof(RowsOrColumns), typeof(int), typeof(FirstDoubleSizePanel), new PropertyMetadata(5, RowsOrColumnsChanged));

        public double ItemHeight
        {
            get
            {
                return (double)GetValue(ItemHeightProperty);
            }
            set
            {
                SetValue(ItemHeightProperty, value);
            }
        }

        public double ItemWidth
        {
            get
            {
                return (double)GetValue(ItemWidthProperty);
            }
            set
            {
                SetValue(ItemWidthProperty, value);
            }
        }

        public Orientation Orientation
        {
            get
            {
                return (Orientation)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        public int RowsOrColumns
        {
            get
            {
                return (int)GetValue(RowsOrColumnsProperty);
            }
            set
            {
                SetValue(RowsOrColumnsProperty, value);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
            }

            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Orientation == Orientation.Horizontal)
            {
                var itemWidth = availableSize.Width / RowsOrColumns;

                var childrenCount = Children.Count;
                if (childrenCount > 0)
                {
                    var firstElement = Children[0];
                    firstElement.Measure(new Size(itemWidth * 2, ItemHeight));
                }
            }
            else
            {
            }

            return base.MeasureOverride(availableSize);
        }

        private static void ItemHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void ItemWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void RowsOrColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
    }
}