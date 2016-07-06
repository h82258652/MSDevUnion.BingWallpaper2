using BingoWallpaper.Models.LeanCloud;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Uwp.Controls
{
    [TemplatePart(Name = CanvasTemplateName, Type = typeof(CanvasControl))]
    public sealed class WallpaperCollectionItem : Control
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource), typeof(WallpaperCollection), typeof(WallpaperCollectionItem), new PropertyMetadata(null, ItemsSourceChanged));

        private const string CanvasTemplateName = "PART_Canvas";

        private CanvasBitmap _backgroundBitmap;

        private CanvasControl _canvas;

        public WallpaperCollectionItem()
        {
            DefaultStyleKey = typeof(WallpaperCollectionItem);

            Unloaded += WallpaperCollectionItem_Unloaded;
        }

        public WallpaperCollection ItemsSource
        {
            get
            {
                return (WallpaperCollection)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvas = (CanvasControl)GetTemplateChild(CanvasTemplateName);
            _canvas.CreateResources += Canvas_CreateResources;
            _canvas.Draw += Canvas_Draw;
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = (WallpaperCollectionItem)d;
            obj._canvas?.Invalidate();
        }

        private void Canvas_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            GaussianBlurEffect blur = new GaussianBlurEffect();
            blur.Source = _backgroundBitmap;
            args.DrawingSession.DrawImage(blur);

            throw new NotImplementedException();
        }

        private async Task CreateResourcesAsync(CanvasControl sender)
        {
            // TODO
            _backgroundBitmap = await CanvasBitmap.LoadAsync(sender, "TODO");
        }

        private void WallpaperCollectionItem_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_canvas != null)
            {
                _canvas.CreateResources -= Canvas_CreateResources;
                _canvas.Draw -= Canvas_Draw;
                _canvas.RemoveFromVisualTree();
                _canvas = null;
            }
        }
    }
}