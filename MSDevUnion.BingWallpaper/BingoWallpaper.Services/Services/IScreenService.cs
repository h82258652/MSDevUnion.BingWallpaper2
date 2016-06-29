using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IScreenService
    {
        Task<uint> GetScreenHeightInRawPixelsAsync();

        Task<uint> GetScreenWidthInRawPixelsAsync();
    }
}