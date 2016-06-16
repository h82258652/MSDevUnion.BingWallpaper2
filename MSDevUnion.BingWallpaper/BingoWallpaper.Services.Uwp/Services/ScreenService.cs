using System;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Services
{
    public class ScreenService : IScreenService
    {
        public async Task<int> GetWidthAsync()
        {
            var webView = new WebView(WebViewExecutionMode.SeparateThread);
            var width = int.Parse(await webView.InvokeScriptAsync("eval", new[]
            {
                "window.screen.width.toString()"
            }));
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)Math.Ceiling(width * scale);
        }

        public async Task<int> GetHeightAsync()
        {
            var webView = new WebView(WebViewExecutionMode.SeparateThread);
            var height = int.Parse(await webView.InvokeScriptAsync("eval", new[]
            {
                "window.screen.height.toString()"
            }));
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)Math.Ceiling(height * scale);
        }
    }
}