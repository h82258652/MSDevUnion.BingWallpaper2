using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

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

        public static readonly DependencyProperty DownloadPercentProperty = DependencyProperty.Register(nameof(DownloadPercent), typeof(double), typeof(ImageEx), new PropertyMetadata(0.0d));

        public double DownloadPercent
        {
            get
            {
                return (double)GetValue(DownloadPercentProperty);
            }
        }

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

        private async void UpdateHttpSource(Uri uri)
        {
            IBuffer buffer;
            using (var client = new HttpClient())
            {
                try
                {
                    var task = client.GetBufferAsync(uri);
                    task.Progress = (info, progressInfo) =>
                    {
                        // TODO
                        // Downloading.

                        DownloadProgress?.Invoke(this, new DownloadProgressEventArgs());
                    };
                    buffer = await task;
                }
                catch (Exception ex)
                {
                    // TODO
                    // Download Failed.

                    _image.Visibility = Visibility.Visible;
                    _placeholderContentControl.Visibility = Visibility.Collapsed;
                    ImageFailed?.Invoke(this, new ExceptionRoutedEventArgs(ex));
                    return;
                }
            }

            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(buffer.AsStream().AsRandomAccessStream());
            _image.Source = bitmap;
            _image.Visibility = Visibility.Visible;
            _placeholderContentControl.Visibility = Visibility.Collapsed;
        }

        private static bool IsHttpUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return uri.IsAbsoluteUri && (uri.Scheme == "http" || uri.Scheme == "https");
        }

        private void UpdateSource(string source)
        {
            if (_image != null && _placeholderContentControl != null)
            {
                if (source == null)
                {
                    _image.Source = null;
                }
                else
                {
                    Uri uri;
                    if (Uri.TryCreate(source, UriKind.RelativeOrAbsolute, out uri))
                    {
                        if (IsHttpUri(uri))
                        {
                            UpdateHttpSource(uri);
                        }
                        else
                        {
                            if (uri.IsAbsoluteUri == false)
                            {
                                Uri.TryCreate("ms-appx:///" + (source.StartsWith("/") ? source.Substring(1) : source), UriKind.Absolute, out uri);
                            }

                            UpdateLocalSource(uri);
                        }
                    }
                    else
                    {
                        _image.Source = null;
                    }
                }
            }
        }

        private void UpdateLocalSource(Uri uri)
        {
            // TODO

            BitmapImage bitmap = new BitmapImage(uri);
            _image.Source = bitmap;
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