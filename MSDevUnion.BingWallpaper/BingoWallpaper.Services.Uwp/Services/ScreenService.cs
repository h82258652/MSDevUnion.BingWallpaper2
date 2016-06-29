using System;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Controls;

namespace BingoWallpaper.Services
{
    public class ScreenService : IScreenService
    {
        public async Task<uint> GetScreenHeightInRawPixelsAsync()
        {
            var webView = new WebView(WebViewExecutionMode.SeparateThread);
            var height = uint.Parse(await webView.InvokeScriptAsync("eval", new[]
            {
                "window.screen.height.toString()"
            }));
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (uint)Math.Ceiling(height * scale);
        }

        public async Task<uint> GetScreenWidthInRawPixelsAsync()
        {
            var webView = new WebView(WebViewExecutionMode.SeparateThread);
            var width = uint.Parse(await webView.InvokeScriptAsync("eval", new[]
            {
                "window.screen.width.toString()"
            }));
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (uint)Math.Ceiling(width * scale);
        }
    }
}