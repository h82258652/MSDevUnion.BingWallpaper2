using BingoWallpaper.Utils;
using BingoWallpaper.Uwp.Extensions;
using BingoWallpaper.Uwp.Utils;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = ImageTemplateName, Type = typeof(Image))]
    [TemplatePart(Name = PlaceholderContentControlTemplateName, Type = typeof(ContentControl))]
    public sealed class ImageEx : Control
    {
        public static readonly DependencyProperty DownloadPercentProperty = DependencyProperty.Register(nameof(DownloadPercent), typeof(double), typeof(ImageEx), new PropertyMetadata(0.0d));

        public static readonly DependencyProperty NineGridProperty = DependencyProperty.Register(nameof(NineGrid), typeof(Thickness), typeof(ImageEx), new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty PlaceholderTemplateProperty = DependencyProperty.Register(nameof(PlaceholderTemplate), typeof(DataTemplate), typeof(ImageEx), new PropertyMetadata(null));

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof(Source), typeof(string), typeof(ImageEx), new PropertyMetadata(null, SourceChanged));

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(ImageEx), new PropertyMetadata(Stretch.Uniform));

        private const string DefaultCacheFolderName = "ImageExCache";

        private const string ImageTemplateName = "PART_Image";

        private const string PlaceholderContentControlTemplateName = "PART_PlaceholderContentControl";

        private static StorageFolder _cacheFolder;

        private Image _image;

        private ContentControl _placeholderContentControl;

        public ImageEx()
        {
            DefaultStyleKey = typeof(ImageEx);
        }

        public event EventHandler<HttpDownloadProgressEventArgs> DownloadProgress;

        public event EventHandler<ExceptionEventArgs> ImageFailed;

        public event RoutedEventHandler ImageOpened;

        public static StorageFolder CacheFolder
        {
            get
            {
                return GetCacheFolder();
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _cacheFolder = value;
            }
        }

        public double DownloadPercent
        {
            get
            {
                return (double)GetValue(DownloadPercentProperty);
            }
            private set
            {
                SetValue(DownloadPercentProperty, value);
            }
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

        public static bool ContainsCache(Uri uri)
        {
            return File.Exists(GetCacheFileName(uri));
        }

        public static void RemoveAllCache()
        {
            // TODO
            try
            {
                Directory.Delete(GetCacheFolder().Path, true);
            }
            catch (Exception)
            {
            }
        }

        public static void RemoveCache(Uri uri)
        {
            var cacheFileName = GetCacheFileName(uri);
            if (File.Exists(cacheFileName))
            {
                File.Delete(cacheFileName);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = (Image)GetTemplateChild(ImageTemplateName);
            _placeholderContentControl = (ContentControl)GetTemplateChild(PlaceholderContentControlTemplateName);
            SetSource(Source);
        }

        private static string GetCacheFileName(Uri uri)
        {
            var originalString = uri.OriginalString;
            var extension = Path.GetExtension(originalString);
            var cacheFileName = HashHelper.GenerateMd5Hash(originalString) + extension;
            return Path.Combine(CacheFolder.Path, cacheFileName);
        }

        private static StorageFolder GetCacheFolder()
        {
            _cacheFolder = _cacheFolder ?? ApplicationData.Current.LocalCacheFolder.CreateFolderAsync(DefaultCacheFolderName, CreationCollisionOption.OpenIfExists).GetAwaiter().GetResult();
            return _cacheFolder;
        }

        private static bool IsHttpUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return uri.IsAbsoluteUri && (uri.Scheme == "http" || uri.Scheme == "https");
        }

        private static void SourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (ImageEx)d;
            var value = (string)e.NewValue;

            obj.SetSource(value);
        }

        private async void SetHttpSource(Uri uri)
        {
            var cacheFileName = GetCacheFileName(uri);

            // TODO
            ImageEx.RemoveAllCache();
            if (File.Exists(cacheFileName) == false)
            {
                _image.Source = null;
                _image.Visibility = Visibility.Collapsed;
                _placeholderContentControl.Visibility = Visibility.Visible;

                byte[] bytes;
                using (var client = new HttpClient())
                {
                    try
                    {
                        var task = client.GetBufferAsync(uri);
                        DownloadPercent = 0;
                        task.Progress = async (info, progressInfo) =>
                        {
                            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                            {
                                DownloadProgress?.Invoke(this, new HttpDownloadProgressEventArgs(progressInfo));
                                if (progressInfo.TotalBytesToReceive.HasValue)
                                {
                                    DownloadPercent = progressInfo.BytesReceived * 100d /
                                                      progressInfo.TotalBytesToReceive.Value;
                                }
                            });
                        };
                        var buffer = await task;
                        bytes = buffer.ToArray();
                        DownloadPercent = 100;
                    }
                    catch (Exception ex)
                    {
                        ImageFailed?.Invoke(this, new ExceptionEventArgs(ex.Message));
                        _image.Visibility = Visibility.Visible;
                        _placeholderContentControl.Visibility = Visibility.Collapsed;
                        return;
                    }
                }

                // 保存图片。
                try
                {
                    await FileExtensions.WriteAllBytesAsync(cacheFileName, bytes);
                }
                catch
                {
                    // ignored
                }

                var bitmap = new BitmapImage();
                bitmap.ImageOpened += (sender, e) =>
                {
                    ImageOpened?.Invoke(this, e);
                };
                bitmap.ImageFailed += (sender, e) =>
                {
                    ImageFailed?.Invoke(this, new ExceptionEventArgs(e.ErrorMessage));
                };
                await bitmap.SetSourceAsync(new MemoryStream(bytes).AsRandomAccessStream());
                _image.Source = bitmap;
                _image.Visibility = Visibility.Visible;
                _placeholderContentControl.Visibility = Visibility.Collapsed;
            }
            else
            {
                var bitmap = new BitmapImage(new Uri(cacheFileName, UriKind.Absolute));
                bitmap.ImageOpened += (sender, e) =>
                {
                    ImageOpened?.Invoke(this, e);
                };
                bitmap.ImageFailed += (sender, e) =>
                {
                    ImageFailed?.Invoke(this, new ExceptionEventArgs(e.ErrorMessage));
                };
                _image.Source = bitmap;
            }
        }

        private void SetLocalSource(Uri uri)
        {
            var bitmap = new BitmapImage(uri);
            bitmap.ImageOpened += (sender, e) =>
            {
                ImageOpened?.Invoke(this, e);
            };
            bitmap.ImageFailed += (sender, e) =>
            {
                ImageFailed?.Invoke(this, new ExceptionEventArgs(e.ErrorMessage));
            };
            _image.Source = bitmap;
        }

        private void SetSource(string source)
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
                            SetHttpSource(uri);
                        }
                        else
                        {
                            if (uri.IsAbsoluteUri == false)
                            {
                                Uri.TryCreate("ms-appx:///" + (source.StartsWith("/") ? source.Substring(1) : source), UriKind.Absolute, out uri);
                            }

                            SetLocalSource(uri);
                        }
                    }
                    else
                    {
                        _image.Source = null;
                    }
                }
            }
        }
    }
}