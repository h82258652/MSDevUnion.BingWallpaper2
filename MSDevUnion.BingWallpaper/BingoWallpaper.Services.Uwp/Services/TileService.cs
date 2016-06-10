using BingoWallpaper.Models;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BingoWallpaper.Services
{
    public class TileService : ITileService
    {
        private readonly IWallpaperService _wallpaperService;

        public TileService(IWallpaperService wallpaperService)
        {
            _wallpaperService = wallpaperService;
        }

        public void UpdatePrimaryTile(Wallpaper wallpaper)
        {
            var document = new XmlDocument();

            // tile 根节点。
            var tile = document.CreateElement("tile");
            document.AppendChild(tile);

            // visual 元素。
            var visual = document.CreateElement("visual");
            tile.AppendChild(visual);

            // Medium, 150x150。
            {
                // binding
                var binding = document.CreateElement("binding");
                binding.SetAttribute("template", "TileMedium");
                visual.AppendChild(binding);

                // image
                var image = document.CreateElement("image");
                image.SetAttribute("src", _wallpaperService.GetUrl(wallpaper.Image, new WallpaperSize(150, 150)));
                image.SetAttribute("placement", "background");
                binding.AppendChild(image);

                // text
                var text = document.CreateElement("text");
                text.AppendChild(document.CreateTextNode(wallpaper.Archive.Info));
                text.SetAttribute("hint-wrap", "true");
                binding.AppendChild(text);
            }

            // Wide, 310x150。
            {
                // binding
                var binding = document.CreateElement("binding");
                binding.SetAttribute("template", "TileWide");
                visual.AppendChild(binding);

                // image
                var image = document.CreateElement("image");
                image.SetAttribute("src", _wallpaperService.GetUrl(wallpaper.Image, new WallpaperSize(310, 150)));
                image.SetAttribute("placement", "background");
                binding.AppendChild(image);

                // text
                var text = document.CreateElement("text");
                text.AppendChild(document.CreateTextNode(wallpaper.Archive.Info));
                text.SetAttribute("hint-wrap", "true");
                binding.AppendChild(text);
            }

            var tileNotification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}