using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = PartImageName, Type = typeof(Image))]
    [TemplatePart(Name = PartPlaceholderContentControlName, Type = typeof(ContentControl))]
    public sealed class ImageEx : Control
    {
        private const string PartImageName = "PART_Image";

        private const string PartPlaceholderContentControlName = "PART_PlaceholderContentControl";

        public static readonly DependencyProperty NineGridProperty = DependencyProperty.Register(nameof(NineGrid), typeof(Thickness), typeof(ImageEx), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty PlaceholderTemplateProperty = DependencyProperty.Register(nameof(PlaceholderTemplate), typeof(DataTemplate), typeof(ImageEx), new PropertyMetadata(null));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(string), typeof(ImageEx), new PropertyMetadata(null, SourceChanged));

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(ImageEx), new PropertyMetadata(Stretch.Uniform));

        public ImageEx()
        {
            DefaultStyleKey = typeof(ImageEx);
        }

        public Thickness NineGrid
        {
            get
            {
                return (Thickness)GetValue(NineGridProperty);
            }
            set
            {
                SetValue(NineGridProperty, value);
            }
        }

        public DataTemplate PlaceholderTemplate
        {
            get
            {
                return (DataTemplate)GetValue(PlaceholderTemplateProperty);
            }
            set
            {
                SetValue(PlaceholderTemplateProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = (Image)GetTemplateChild(PartImageName);
            _placeholderContentControl = (ContentControl)GetTemplateChild(PartPlaceholderContentControlName);
            UpdateSource(Source);
        }

        private void UpdateSource(string source)
        {
            if (_image != null && _placeholderContentControl != null)
            {
                Uri uri;
                if (source == null || Uri.TryCreate(source, UriKind.Absolute, out uri) == false)
                {
                    _image.Source = null;
                    _image.Visibility = Visibility.Visible;
                    _placeholderContentControl.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var bitmap = new BitmapImage();
                    bitmap.DownloadProgress += (sender, e) =>
                    {
                        DownloadProgress?.Invoke(this, e);
                    };
                    bitmap.ImageOpened += (sender, e) =>
                    {
                        ImageOpened?.Invoke(this, e);
                    };
                    bitmap.ImageFailed += (sender, e) =>
                    {
                        ImageFailed?.Invoke(this, e);
                    };
                    bitmap.UriSource = uri;
                    _image.Source = bitmap;
                }
            }
        }

        public event ExceptionRoutedEventHandler ImageFailed;

        public event DownloadProgressEventHandler DownloadProgress;

        public event RoutedEventHandler ImageOpened;

        private Image _image;

        private ContentControl _placeholderContentControl;

        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }
            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public Stretch Stretch
        {
            get
            {
                return (Stretch)GetValue(StretchProperty);
            }
            set
            {
                SetValue(StretchProperty, value);
            }
        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (ImageEx)d;
            var value = (string)e.NewValue;

            obj.UpdateSource(value);
        }
    }
}