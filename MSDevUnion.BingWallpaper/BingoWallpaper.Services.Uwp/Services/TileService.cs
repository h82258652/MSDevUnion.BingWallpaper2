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

        public void UpdatePrimaryTile(IImage image, string text)
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
                var imageElement = document.CreateElement("image");
                imageElement.SetAttribute("src", _wallpaperService.GetUrl(image, new WallpaperSize(150, 150)));
                imageElement.SetAttribute("placement", "background");
                binding.AppendChild(imageElement);

                // text
                var textElement = document.CreateElement("text");
                textElement.AppendChild(document.CreateTextNode(text));
                textElement.SetAttribute("hint-wrap", "true");
                binding.AppendChild(textElement);
            }

            // Wide, 310x150。
            {
                // binding
                var binding = document.CreateElement("binding");
                binding.SetAttribute("template", "TileWide");
                visual.AppendChild(binding);

                // image
                var imageElement = document.CreateElement("image");
                imageElement.SetAttribute("src", _wallpaperService.GetUrl(image, new WallpaperSize(310, 150)));
                imageElement.SetAttribute("placement", "background");
                binding.AppendChild(imageElement);

                // text
                var textElement = document.CreateElement("text");
                textElement.AppendChild(document.CreateTextNode(text));
                textElement.SetAttribute("hint-wrap", "true");
                binding.AppendChild(textElement);
            }

            var tileNotification = new TileNotification(document);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
    }
}