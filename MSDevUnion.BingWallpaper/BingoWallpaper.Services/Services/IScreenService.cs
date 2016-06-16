using System.Threading.Tasks;

namespace BingoWallpaper.Services
{
    public interface IScreenService
    {
        Task<int> GetWidthAsync();

        Task<int> GetHeightAsync();
    }
}